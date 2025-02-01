using System.ComponentModel;
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using RaCruds.Config;
using RaCruds.Extensions;
using RaCruds.Models;
using RaCruds.Models.Specifications;
using RaCruds.Models.Statements;

namespace RaCruds.DirectDbContext;
internal class EntityContextFilterOperation<TOutDto, TEntity> : IFilterFromDbContextEntityOperation<TOutDto, TEntity>
        where TEntity : class
        where TOutDto : class
{
    private readonly IMapper _mapper;

    private readonly EntityContextFilterOperationParameters _parameters;

    private readonly IDictionary<string, Type> _customSpecifications;

    public EntityContextFilterOperation(IMapper mapper, IFilterDescriptorContainer filterDescriptorContainer)
    {
        _mapper = mapper;
        var descriptor = filterDescriptorContainer.GetFor<TOutDto>();

        _parameters = (EntityContextFilterOperationParameters)descriptor.EntityFilterOperationParameters;
        _customSpecifications = descriptor.CustomSpecifications;
    }

    public async Task<PagingResult<TOutDto>> FilterAsync(
        FromDbContextFilterableOperationParameters<TEntity> operationParameters,
        FilterParameters filterParameters, CancellationToken cancellationToken = default)
    {
        var statements = filterParameters.Statements;
        var predicate = LinqKit.PredicateBuilder.New<TEntity>(true);

        StatementsToPredicates(statements, predicate);

        var count = await operationParameters.DbContext.Set<TEntity>().AsQueryable()
            .Where(predicate).LongCountAsync(cancellationToken);

        var entityQuery = operationParameters.DbContext.Set<TEntity>().AsQueryable();

        if (_parameters.IncludeProperties != null)
        {
            foreach (var includedProperty in _parameters.IncludeProperties)
            {
                entityQuery = entityQuery.Include(includedProperty);
            }
        }

        var query = entityQuery.AsExpandable().Where(predicate);

        var orderBy = filterParameters.OrderBy;

        if (string.IsNullOrWhiteSpace(orderBy))
        {
            orderBy = _parameters.DefaultOrderColumn;
        }

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var sortOrder = filterParameters.OrderKind == OrderKind.Asc
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            var orderByColumn = orderBy.ToLower();

            if (_parameters.FieldsMapping.ContainsKey(orderByColumn))
            {
                orderByColumn = _parameters.FieldsMapping[orderByColumn];
            }

            query = query.OrderBy(orderByColumn, sortOrder);
        }

        var pageData =
            PageDataCalculator.GetPageData(filterParameters.PageSize, filterParameters.CurrentPage, true);

        if (pageData.skip >= 0)
        {
            query = query.Skip(pageData.skip).Take(pageData.take);
        }

        TOutDto[] entities;

        if (_parameters.DirectProject)
        {
            entities = await _mapper.ProjectTo<TOutDto>(query).ToArrayAsync(cancellationToken);
        }
        else
        {
            var queryEntities = await query.ToArrayAsync(cancellationToken);

            entities = _mapper.Map<TOutDto[]>(queryEntities);
        }

        return new PagingResult<TOutDto>(count, entities);
    }

    private void StatementsToPredicates(FilterStatement[] statements, ExpressionStarter<TEntity> predicate)
    {
        foreach (var filter in statements)
        {
            if (filter.Statements != null && filter.Statements.Any())
            {
                var childPredicate = LinqKit.PredicateBuilder.New<TEntity>(true);
                StatementsToPredicates(filter.Statements, childPredicate);

                if (filter.LogicalOperator == FilterLogicalOperators.And)
                {
                    predicate.And(childPredicate);
                }
                else if (filter.LogicalOperator == FilterLogicalOperators.Or)
                {
                    predicate.Or(childPredicate);
                }

                continue;
            }

            ISpecification<TEntity> specification = Create(filter);

            if (specification == null)
            {
                continue;
            }

            if (filter.LogicalOperator == FilterLogicalOperators.Or)
            {
                predicate.Or(specification.ToExpression());
            }
            else
            {
                predicate.And(specification.ToExpression());
            }
        }
    }

    private ISpecification<TEntity>? Create(FilterStatement filter)
    {
        var propertyName = filter.ParameterName.ToLower();

        ISpecification<TEntity>? customSpecification =
            CreateCustom(filter.ComparisonOperator, filter.ParameterName, filter.ParameterValue);
        
        if (customSpecification is not null)
        {
            return customSpecification;
        }

        if (_parameters.FieldsMapping.ContainsKey(propertyName))
        {
            propertyName = _parameters.FieldsMapping[propertyName];
        }

        var property = typeof(TEntity).GetExtendedPropertyType(propertyName);

        if (property == null)
        {
            return null;
        }

        var propertyValue = filter.ParameterValue;
        var comparisonOperator = filter.ComparisonOperator;

        var targetType = property.PropertyType;

        object value = FilterTypeCorrector.ChangeType<TEntity>(propertyName, propertyValue);

        if (value == null)
        {
            value = FilterTypeCorrector.GetDefaultValue<TEntity>(propertyName);
        }

        if (value == null)
        {
            return null;
        }

        if (string.Equals(comparisonOperator, FilterComparisonOperators.Equals,StringComparison.OrdinalIgnoreCase))

        {
            return new EqualsSpecification<TEntity>(targetType, value, propertyName);
        }

        
        if (string.Equals(comparisonOperator, FilterComparisonOperators.GreaterThan, StringComparison.OrdinalIgnoreCase))
        {
            return new GreaterThanSpecification<TEntity>(targetType, value, propertyName);
        }

        if (string.Equals(comparisonOperator, FilterComparisonOperators.GreaterThanOrEqual, StringComparison.OrdinalIgnoreCase))
        {
            return new GreaterThanOrEqualSpecification<TEntity>(targetType, value, propertyName);
        }

        if (string.Equals(comparisonOperator, FilterComparisonOperators.LessThan, StringComparison.OrdinalIgnoreCase))
        {
            return new LessThanSpecification<TEntity>(targetType, value, propertyName);
        }

        if (string.Equals(comparisonOperator, FilterComparisonOperators.LessThanOrEqual, StringComparison.OrdinalIgnoreCase))
            if (comparisonOperator == FilterComparisonOperators.LessThanOrEqual)
        {
            return new LessThanOrEqualSpecification<TEntity>(targetType, value, propertyName);
        }

        if (string.Equals(comparisonOperator, FilterComparisonOperators.Contains, StringComparison.OrdinalIgnoreCase))
        {
            return new ContainsSpecification<TEntity>(propertyValue, propertyName);
        }

        return null;
    }

    private ISpecification<TEntity>? CreateCustom(string comparisonOperator, string propertyName, string propertyValue)
    {
        if (!_customSpecifications.TryGetValue(comparisonOperator, out Type? specType))
        {
            return null;
        }

        return (ISpecification<TEntity>?)Activator.CreateInstance(specType, args: [propertyName, propertyValue]);
    }
}

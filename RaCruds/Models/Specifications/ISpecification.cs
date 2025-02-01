using System.Linq.Expressions;

namespace RaCruds.Models.Specifications;
public interface ISpecification<T>
    where T : class
{
    Expression<Func<T, bool>> ToExpression();
}

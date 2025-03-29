function searchByRa() {
    let user = document.getElementById("userNameRa").value;
    let total = document.getElementById("cartTotalRa").value;
    let itemName = document.getElementById("cartItemNameRa").value;
    let status = document.getElementById("orderStatusRa").value;
    let orderBy = document.getElementById("orderByRa").value;
    let orderByIsDescending = document.getElementById("orderByIsDescendingRa").checked;
    let page = Number(document.getElementById("pageRa").value);
    let pageItemsCount = Number(document.getElementById("pageItemsCountRa").value);

    let query = "";

    let statements = [];

    addRaFilter(statements, user, "userName", "contains");
    addRaFilter(statements, total, "total", "greaterThanOrEqual");
    addRaFilter(statements, status, "order.status", "equals");
    addRaFilter(statements, itemName, "items", "cartItemName");
    
    if (statements.length !== 0) {
        query = "?statements=" + JSON.stringify(statements);;
    }

    if (pageItemsCount > 0 && pageItemsCount < 100 && page >= 0) {
        if (query.length == 0) {
            query += "?"
        } else {
            query += "&"
        }

        query += `page=${page}&pageSize=${pageItemsCount}`;
    }

    if (!!orderBy && orderBy.length > 0) {
        if (query.length == 0) {
            query += "?"
        } else {
            query += "&"
        }
        if (orderByIsDescending == true) {
            query += `orderBy=${orderBy}&orderKind=desc`;
        } else {
            query += `orderBy=${orderBy}&orderKind=asc`;
        }
    }

    fetch('raCarts' + query, {
        method: 'GET'
    })
        .then(response => response.json())
        .then(result => render(result["entities"], result["count"]))
        .catch(error => console.error("Unable get carts data by ra filters", error))
}

function addRaFilter(statements, value, parameterName, comparisonOperator, changeTypeFunct) {
    if (!!value && value.length > 0) {

        if (!!changeTypeFunct) {
            value = changeTypeFunct(value);
        }

        const newStatement = {            
            spec: comparisonOperator,
            op: "and",
            name: parameterName,
            value: value
        };
        statements.push(newStatement);
    }
}

function searchBySieve() {
    let user = document.getElementById("userNameSieve").value;
    let total = document.getElementById("cartTotalSieve").value;
    let itemName = document.getElementById("cartItemNameSieve").value;
    let status = document.getElementById("orderStatusSieve").value;
    let orderBy = document.getElementById("orderBySieve").value;
    let orderByIsDescending = document.getElementById("orderByIsDescendingSieve").checked;
    let page = Number(document.getElementById("pageSieve").value);
    let pageItemsCount = Number(document.getElementById("pageItemsCountSieve").value);

    let query = "";
    query = concatSieveFilter(query, `userName@=${user}`, user);
    query = concatSieveFilter(query, `order.status==${status}`, status);
    query = concatSieveFilter(query, `total>=${total}`, total);
    query = concatSieveFilter(query, `CartItemName@=${itemName}`, itemName);

    if (query.length > 0) {
        query = "?filters=" + query;
    }

    if (pageItemsCount > 0 && pageItemsCount < 100 && page >= 0) {
        if (query.length == 0) {
            query += "?"
        } else {
            query += "&"
        }

        query += `page=${page}&pageSize=${pageItemsCount}`;
    }

    if (!!orderBy && orderBy.length > 0) {
        if (query.length == 0) {
            query += "?"
        } else {
            query += "&"
        }
        if (orderByIsDescending == true) {
            query += `sorts=-${orderBy}`;
        } else {
            query += `sorts=${orderBy}`;
        }
    }

    fetch('sieveCarts' + query, {
        method: 'GET'
    })
        .then(response => response.json())
        .then(result => render(result["items"], result["count"]))
        .catch(error => console.error("Unable get carts data by sieve filters", error))
}

function concatSieveFilter(filter, expression, value) {
    let result = filter;
    if (!!value && value.length > 0) {

        if (result.length > 0) {
            result += ",";
        }

        result += expression
    }
    return result;
}

function searchByOdata() {
    let user = document.getElementById("userNameOdata").value;
    let total = document.getElementById("cartTotalOdata").value;
    let itemName = document.getElementById("cartItemNameOdata").value;
    let status = document.getElementById("orderStatusOdata").value;
    let orderBy = document.getElementById("orderByOdata").value;
    let orderByIsDescending = document.getElementById("orderByIsDescendingOdata").checked;
    let page = Number(document.getElementById("pageOdata").value);
    let pageItemsCount = Number(document.getElementById("pageItemsCountOdata").value);

    let query = "";

    query = concatOdataFilter(query, `contains(userName, '${user}')`, user);
    query = concatOdataFilter(query, `Order/status eq '${status}'`, status);
    query = concatOdataFilter(query, `Total gt ${total}`, total);
    query = concatOdataFilter(query, `cartItems/Any(w:contains(w/name,'${itemName}'))`, itemName);
   
    if (query.length > 0) {
        query = "&filter=" + query;
    }

    if (!!orderBy && orderBy.length > 0) {
        query += "&$orderby=" + orderBy;

        if (orderByIsDescending == true ) {
            query += " desc";
        }
    }

    if (pageItemsCount > 0 && pageItemsCount < 100 && page >= 0) {
        let skip = page * pageItemsCount;
        query += `&$skip=${skip}&$top=${pageItemsCount}`;
    }


    fetch('odata/OdataCarts?$count=true&$expand=cartItems,order' + query, {
        method: 'GET'
    })
        .then(response => response.json())
        .then(result => render(result.value, result["@odata.count"]))
        .catch(error => console.error("Unable get carts data by odata filters", error))
}

function concatOdataFilter(filter, expression, value){
    let result = filter;
    if (!!value && value.length > 0) {

        if (result.length > 0) {
            result += " and ";
        }

        result += expression
    }
    return result;
}

let statusMap = {
    "init": "Оформление",
    "paid": "Оплачен",
    "fail": "Ошибка",
    "refunded": "Возврат"
};
function render(carts, totalCount) {
    let table = document.getElementById('carts');
    let output = "";
    if (!!carts && carts.length) {
        for (var i = 0; i < carts.length; i++) {
            let cart = carts[i];

            let status = "-";

            if (!!cart.order) {
                status = statusMap[cart.order.status];
            }

            output +=
                `  <tr>
                     <td>${status}</td>
                      <td>${cart.userName}</td>
                      <td>${cart.total}</td>
                      <td>${cart.cartItems.map(a => a.name).join('<br/>')}</td>
                  </tr>
                `;
        }
    }

    table.innerHTML = output;

    let totalCell = document.getElementById('totalFound');
    totalCell.innerHTML = totalCount;

}
namespace MySales.Product.Api.Domain.Interfaces.Repositories.Query
{
    public interface IQueryOrder<T>
    {
        IQueryResult<T> OrderBy(string orderColumn, string orderType = "asc");
    }
}

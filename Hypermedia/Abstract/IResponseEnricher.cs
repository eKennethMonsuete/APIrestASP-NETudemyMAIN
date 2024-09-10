using Microsoft.AspNetCore.Mvc.Filters;

namespace APIrestASP_NETudemy.Hypermedia.Abstract
{
    public interface IResponseEnricher
    {

        bool CanEnrich(ResultExecutingContext context);
        Task Enrich(ResultExecutingContext context);
    }
}

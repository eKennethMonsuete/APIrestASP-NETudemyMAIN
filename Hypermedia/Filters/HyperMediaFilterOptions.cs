using APIrestASP_NETudemy.Hypermedia.Abstract;

namespace APIrestASP_NETudemy.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList
        { get; set; } = new List<IResponseEnricher>();
    }
}

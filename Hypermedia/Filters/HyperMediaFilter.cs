﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIrestASP_NETudemy.Hypermedia.Filters
{
    public class HyperMediaFilter : ResultFilterAttribute
    {
        private readonly HyperMediaFilterOptions _options;

        public HyperMediaFilter(HyperMediaFilterOptions options)
        {
        
        _options = options;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void TryEnrichResult(ResultExecutingContext context)
        {
            if(context.Result is OkObjectResult result) 
                {
                var enricher = _options.ContentResponseEnricherList.FirstOrDefault(x=> x.CanEnrich(context));
                if(enricher != null) Task.FromResult(enricher.Enrich(context));
                }
        }
    }
}

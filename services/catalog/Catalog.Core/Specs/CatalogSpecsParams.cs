using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Core.Specs
{
    public class CatalogSpecsParams
    {
        private const int MaxPageSize = 50;

        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public string? Sort { get; set; }
        public string? BrandId { get; set; }
        public string? TypeId { get; set; }
        public string? Search { get; set; }

        public int GetSkip() => (PageNumber - 1) * PageSize;
        public int GetTake() => Math.Min(PageSize, MaxPageSize);
    }
}

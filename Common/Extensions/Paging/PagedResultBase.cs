using System.Collections.Generic;

namespace Inventory.Common.Behaviours.Extensions.Paging
{
    public abstract class PagedResultBase
    {
        public IEnumerable<int> Pages { get; set; }

        public int EndIndex { get; set; }

        public int StartIndex { get; set; }

        public int EndPage { get; set; }

        public int StartPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public int CurrentPage { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }

        public int RowCount { get; set; }
    }
}
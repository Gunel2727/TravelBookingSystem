using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TBS.Domain.Common
{
    public class PagedList<T>
    {
        public IReadOnlyCollection<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

       
        public PagedList()
        {
            Items = new List<T>();
        }

       
        [JsonConstructor]
        public PagedList(IReadOnlyCollection<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
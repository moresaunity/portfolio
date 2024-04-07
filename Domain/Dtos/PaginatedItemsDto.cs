using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class PaginatedItemsDto<TEntity> where TEntity: class
    {
        public PaginatedItemsDto(int PageIndex, int PageSize, long Count, IEnumerable<TEntity> Data)
        {
            this.PageIndex = PageIndex;
            this.PageSize = PageSize;
            this.Count = Count;
            this.Data = Data;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}

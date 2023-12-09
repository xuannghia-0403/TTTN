using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbOrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }
        public decimal? Amount { get; set; }
    }
}

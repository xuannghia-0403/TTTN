using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbProductSale
    {
        public int ProductId { get; set; }
        public decimal? PriceSale { get; set; }
        public int? Qty { get; set; }
        public DateTime? NgayBd { get; set; }
        public DateTime? NgayKt { get; set; }
        public byte? Status { get; set; }
    }
}

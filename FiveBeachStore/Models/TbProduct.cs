using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbProduct
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceSale { get; set; }
        public string? Image { get; set; }
        public int? Qty { get; set; }
        public string? Detail { get; set; }
        public string? Metakey { get; set; }
        public string? Metadesc { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public byte? Status { get; set; }

        public virtual TbCategory? Category { get; set; }
    }
}

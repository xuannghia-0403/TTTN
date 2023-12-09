using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbProductImage
    {
        public int ProductId { get; set; }
        public string? Image { get; set; }
        public string? Metadesc { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public byte? Status { get; set; }
    }
}

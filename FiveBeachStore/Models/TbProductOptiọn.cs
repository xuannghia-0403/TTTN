using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbProductOptiọn
    {
        public int ProductId { get; set; }
        public int? Color { get; set; }
        public int? Memorysize { get; set; }
        public int? Qty { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public byte? Status { get; set; }
    }
}

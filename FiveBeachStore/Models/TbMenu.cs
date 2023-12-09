using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbMenu
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public int? TableId { get; set; }
        public string? Type { get; set; }
        public int? SortOrder { get; set; }
        public string? Position { get; set; }
        public int? Level { get; set; }
        public int? ParentId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public byte? Status { get; set; }
    }
}

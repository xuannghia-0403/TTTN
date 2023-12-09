﻿using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbBrand
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Image { get; set; }
        public int? SortOrder { get; set; }
        public string? Metakey { get; set; }
        public string? Metadesc { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public byte? Status { get; set; }
    }
}

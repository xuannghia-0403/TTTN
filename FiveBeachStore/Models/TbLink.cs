using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbLink
    {
        public int Id { get; set; }
        public string? Link { get; set; }
        public string? Type { get; set; }
        public int? TableId { get; set; }
    }
}

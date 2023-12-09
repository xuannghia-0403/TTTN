using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbRole
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Metadesc { get; set; }
        public byte? Status { get; set; }
    }
}

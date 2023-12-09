using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbConfig
    {
        public int Id { get; set; }
        public string? SiteName { get; set; }
        public string? Metakey { get; set; }
        public string? Metadesc { get; set; }
        public string? Author { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Youtube { get; set; }
        public string? Googleplus { get; set; }
        public byte? Status { get; set; }
    }
}

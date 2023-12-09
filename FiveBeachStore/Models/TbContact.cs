using System;
using System.Collections.Generic;

namespace FiveBeachStore.Models
{
    public partial class TbContact
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? ReplayId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public byte? Status { get; set; }
    }
}

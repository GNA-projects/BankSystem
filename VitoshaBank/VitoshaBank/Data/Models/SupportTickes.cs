using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class SupportTickes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public sbyte HasResponce { get; set; }

        public virtual Users User { get; set; }
    }
}

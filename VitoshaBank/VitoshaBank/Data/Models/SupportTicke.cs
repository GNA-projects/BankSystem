using System;
using System.Collections.Generic;

#nullable disable

namespace VitoshaBank.Data.Models
{
    public partial class SupportTicke
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public sbyte HasResponce { get; set; }

        public virtual User User { get; set; }
    }
}

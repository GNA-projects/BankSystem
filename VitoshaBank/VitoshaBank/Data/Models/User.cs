using System;
using System.Collections.Generic;

#nullable disable

namespace VitoshaBank.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisterDate { get; set; }
        public int? LastTransactionId { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual Transaction LastTransaction { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual Card Card { get; set; }
        public virtual Credit Credit { get; set; }
        public virtual Deposit Deposit { get; set; }
        public virtual SupportTicke SupportTicke { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}

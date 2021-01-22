using System;
using System.Collections.Generic;

namespace VitoshaBank.Data.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisterDate { get; set; }
        public int? LastTransactionId { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }

        public virtual Transactions LastTransaction { get; set; }
        public virtual BankAccounts BankAccounts { get; set; }
        public virtual Cards Cards { get; set; }
        public virtual Credits Credits { get; set; }
        public virtual Deposits Deposits { get; set; }
        public virtual SupportTickets SupportTickets { get; set; }
        public virtual Wallets Wallets { get; set; }
    }
}

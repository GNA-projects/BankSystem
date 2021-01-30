﻿using System;
using System.Collections.Generic;
using VitoshaBank.Services.GenerateCardInfoService;

namespace VitoshaBank.Data.Models
{
    public partial class Cards
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardNumber { get; set; } = GenerateCardInfo.GenerateNumber(15);
        public string Cvv { get; set; } = GenerateCardInfo.GenerateCVV(3);
        public int BankAccountId { get; set; }
        public DateTime CardExiprationDate { get; set; } = DateTime.Now.AddMonths(60);
        public virtual BankAccounts BankAccount { get; set; }
        public virtual Users User { get; set; }
    }
}

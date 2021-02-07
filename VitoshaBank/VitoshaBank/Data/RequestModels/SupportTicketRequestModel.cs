using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VitoshaBank.Data.Models;

namespace VitoshaBank.Data.RequestModels
{
    public class SupportTicketRequestModel
    {
        public SupportTickets Ticket { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
    }
}

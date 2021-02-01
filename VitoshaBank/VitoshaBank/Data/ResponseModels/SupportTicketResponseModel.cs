using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VitoshaBank.Data.ResponseModels
{
    public class SupportTicketResponseModel
    {
        public string Title { get; set; }
        public DateTime TicketDate { get; set; }
        public bool HasResponse { get; set; }
        public string Username { get; set; }
    }
}

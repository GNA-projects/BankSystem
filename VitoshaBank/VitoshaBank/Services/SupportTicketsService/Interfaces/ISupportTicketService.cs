using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;

namespace VitoshaBank.Services.SupportTicketsService.Interfaces
{
    public interface ISupportTicketService
    {
        public Task<ActionResult<MessageModel>> CreateSupportTicket(ClaimsPrincipal currentUser, string username, SupportTickets ticket, BankSystemContext _context, MessageModel _messageModel);
        public Task<ActionResult<ICollection<SupportTicketResponseModel>>> GetUserTicketsInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel messageModel);
        public Task<ActionResult<ICollection<SupportTickets>>> GetAllTicketsInfo(ClaimsPrincipal currentUser, BankSystemContext _context, MessageModel messageModel);
        public Task<ActionResult<MessageModel>> GiveResponse(ClaimsPrincipal currentUser, SupportTickets ticket, BankSystemContext _context, MessageModel _messageModel);
    }
}

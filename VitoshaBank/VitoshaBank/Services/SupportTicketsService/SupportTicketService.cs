using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.ResponseModels;
using VitoshaBank.Services.SupportTicketsService.Interfaces;

namespace VitoshaBank.Services.SupportTicketsService
{
    public class SupportTicketService : ControllerBase, ISupportTicketService
    {
        public async Task<ActionResult<MessageModel>> CreateSupportTicket(ClaimsPrincipal currentUser, string username, SupportTickets ticket, BankSystemContext _context, MessageModel _messageModel)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {

                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

                if (userAuthenticate != null)
                {
                    if (ticket.Title == null || ticket.Message==null)
                    {
                        _messageModel.Message = "Ticket must have title and message!";
                        return StatusCode(400, _messageModel);
                    }
                    ticket.UserId = userAuthenticate.Id;
                    ticket.Date = DateTime.Now;
                    ticket.HasResponce = false;
                    _context.Add(ticket);
                    await _context.SaveChangesAsync();

                    _messageModel.Message = "Ticket created succesfully";
                    return StatusCode(200, _messageModel);
                }
                else
                {
                    _messageModel.Message = "User not found";
                    return StatusCode(404, _messageModel);
                }
            }
            else
            {
                _messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(400, _messageModel);
            }

        }
        public async Task<ActionResult<ICollection<SupportTicketResponseModel>>> GetUserTicketsInfo(ClaimsPrincipal currentUser, string username, BankSystemContext _context, MessageModel messageModel)
        {
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                List<SupportTicketResponseModel> userTickets = new List<SupportTicketResponseModel>();
                var userAuthenticate = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
                if (userAuthenticate == null)
                {
                    messageModel.Message = "User not found";
                    return StatusCode(404, messageModel);
                }
                else
                {
                    foreach (var ticket in _context.SupportTickets)
                    {
                        if (ticket.UserId == userAuthenticate.Id)
                        {
                            SupportTicketResponseModel responseModel = new SupportTicketResponseModel();

                            responseModel.Title = ticket.Title;
                            responseModel.Message = ticket.Message;
                            responseModel.TicketDate = ticket.Date;
                            responseModel.HasResponse = ticket.HasResponce;
                            userTickets.Add(responseModel);
                        }
                    }
                }

                if (userTickets.Count != 0)
                {

                    return StatusCode(200, userTickets);
                }
            }
            else
            {
                messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(403, messageModel);
            }

            messageModel.Message = "You don't have Support Tickets!";
            return StatusCode(400, messageModel);
        }
        public async Task<ActionResult<ICollection<SupportTickets>>> GetAllTicketsInfo(ClaimsPrincipal currentUser, BankSystemContext _context, MessageModel messageModel)
        {
            string role = "";
            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                string userRole = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Roles").Value;
                role = userRole;
            }
            if (role == "Admin")
            {
                List<SupportTickets> allTickets = new List<SupportTickets>();
                foreach (var ticket in _context.SupportTickets)
                {
                    if (ticket.HasResponce == false)
                    {
                        allTickets.Add(ticket);
                    }
                }
                if (allTickets.Count != 0)
                {
                    return StatusCode(200, allTickets);
                }
            }
            else
            {
                messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(403, messageModel);
            }

            messageModel.Message = "You don't have Support Tickets!";
            return StatusCode(400, messageModel);
        }
        public async Task<ActionResult<MessageModel>> GiveResponse(ClaimsPrincipal currentUser, int id, BankSystemContext _context, MessageModel _messageModel)
        {
            string role = "";

            if (currentUser.HasClaim(c => c.Type == "Roles"))
            {
                string userRole = currentUser.Claims.FirstOrDefault(currentUser => currentUser.Type == "Roles").Value;
                role = userRole;
            }
            if (role == "Admin")
            {
                var ticketExists = await _context.SupportTickets.FirstOrDefaultAsync(p => p.Id == id);

                if (ticketExists != null)
                {
                    ticketExists.HasResponce = true;
                    
                    await _context.SaveChangesAsync();
                    _messageModel.Message = "Responded to ticket succesfully!";
                    return StatusCode(200, _messageModel);
                }
                _messageModel.Message = "Ticket not found!";
                return StatusCode(404, _messageModel);

            }
            else
            {
                _messageModel.Message = "You are not authorized to do such actions";
                return StatusCode(400, _messageModel);
            }

        }
    }
}

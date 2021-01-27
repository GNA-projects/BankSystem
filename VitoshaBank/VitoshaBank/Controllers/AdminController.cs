using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VitoshaBank.Data.MessageModels;
using VitoshaBank.Data.Models;
using VitoshaBank.Data.RequestModels;
using VitoshaBank.Services.BankAccountService.Interfaces;
using VitoshaBank.Services.CreditService.Interfaces;
using VitoshaBank.Services.DebitCardService.Interfaces;
using VitoshaBank.Services.DepositService;
using VitoshaBank.Services.IBANGeneratorService.Interfaces;
using VitoshaBank.Services.Interfaces.UserService;
using VitoshaBank.Services.Interfaces.WalletService;
using VitoshaBank.Services.TransactionService.Interfaces;

namespace VitoshaBank.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly BankSystemContext _context;
        private readonly ILogger<BankAccounts> _logger;
        private readonly IBCryptPasswordHasherService _BCrypt;
        private readonly IConfiguration _config;
        private readonly IBankAccountService _bankAccountService;
        private readonly IDebitCardService _debitCardService;
        private readonly IIBANGeneratorService _IBAN;
        private readonly ITransactionService _transactionService;
        private readonly IUsersService _userService;
        private readonly ICreditService _creditService;
        private readonly IDepositService _depositService;
        private readonly IWalletsService _walletService;
        private readonly MessageModel _messageModel;

        public AdminController(BankSystemContext context, ILogger<BankAccounts> logger, IBankAccountService bankAccountService, IIBANGeneratorService IBAN, IDebitCardService debitCardService, ITransactionService transactionService, IBCryptPasswordHasherService bCrypt, IConfiguration config, IUsersService usersService, ICreditService creditService, IDepositService depositService, IWalletsService walletsService)
        {
            _context = context;
            _logger = logger;
            _bankAccountService = bankAccountService;
            _debitCardService = debitCardService;
            _IBAN = IBAN;
            _transactionService = transactionService;
            _messageModel = new MessageModel();
            _BCrypt = bCrypt;
            _userService = usersService;
            _creditService = creditService;
            _depositService = depositService;
            _config = config;
            _walletService = walletsService;

        }

        [HttpPost("create/bankaccount")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> CreateBankAccount(BankAccountRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.CreateBankAccount(currentUser, requestModel.Username, requestModel.BankAccount, _IBAN, _context, _debitCardService, _messageModel);
        }

        [HttpPut("addmoney/bankaccount")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> AddMoneyInBankAccount(BankAccountRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.AddMoney(requestModel.BankAccount, currentUser, requestModel.Username, requestModel.Amount, _context, _transactionService, _messageModel);
        }
        [HttpDelete("delete/bankaccount")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteBankAccount(BankAccountRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _bankAccountService.DeleteBankAccount(currentUser, requestModel.Username, _context, _messageModel);
        }
        [HttpGet("get/user/{username}")]
        [Authorize]
        public async Task<ActionResult<Users>> GetUser(string username)
        {
            var currentUser = HttpContext.User;
            return await _userService.GetUser(currentUser, username, _context, _messageModel);
        }
        [HttpDelete("delete/user")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteUser(UserRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _userService.DeleteUser(currentUser, requestModel.Username, _context, _messageModel);
        }
        [HttpPost("create/credit")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> CreateCredit(CreditRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _creditService.CreateCredit(currentUser, requestModel.Username, requestModel.Credit, requestModel.Period, _IBAN, _context, _messageModel);
        }
        [HttpDelete("delete/credit")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteCredit(CreditRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _creditService.DeleteCredit(currentUser, requestModel.Username, _context, _messageModel);
        }
        [HttpPost("create/debitcard")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> CreateDebitcard(DebitCardRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _debitCardService.CreateDebitCard(currentUser, requestModel.Username, requestModel.BankAccount, _context, requestModel.Card, _messageModel);
        }
        [HttpDelete("delete/debitcard")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteDebitCard(DebitCardRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _debitCardService.DeleteDebitCard(currentUser, requestModel.Username, _context, _messageModel);
        }
        [HttpPost("create/deposit")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> CreateDeposit(DepositRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _depositService.CreateDeposit(currentUser, requestModel.Username, requestModel.Deposit, _IBAN, _context, _messageModel);
        }
        [HttpDelete("delete/deposit")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteBankAccount(DepositRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _depositService.DeleteDeposit(currentUser, requestModel.Username, _context, _messageModel);
        }
        [HttpPost("create/wallet")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> CreateWallet(WalletRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _walletService.CreateWallet(currentUser, requestModel.Username, requestModel.Wallet, _IBAN, _context, _messageModel);
        }
        [HttpDelete("delete/wallet")]
        [Authorize]
        public async Task<ActionResult<MessageModel>> DeleteWallet(WalletRequestModel requestModel)
        {
            var currentUser = HttpContext.User;
            return await _walletService.DeleteWallet(currentUser, requestModel.Username, _context, _messageModel);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Hosts.Api.Controllers
{
    using Application.Contracts.DatabaseSessions;
    using Application.Domain.Models;
    using Application.Hosts.Api.Models;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BudgetController : ControllerBase
    {

        private readonly IDbAccess dbAccess;
        public BudgetController(IDbAccess _dbAccess)
        {
            if (_dbAccess == null)
                throw new ArgumentNullException(nameof(_dbAccess));
            dbAccess = _dbAccess;

        }

        [HttpPost("AllocateBudget")]
        [Authorize("Admin")]
        public ActionResult AllocateBudget([FromQuery] decimal amount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (amount <= 0)
                return BadRequest("Invalid Amount");

            var responseObject = new ResponseInfo<string>();
            try
            {
                //For Transaction Integrity, Every transaction should have a debit entry and a corresponding credit entry.
                //Accounts here are hard coded for demonstration purposes
                var tranId = Guid.NewGuid().ToString();
                Transaction debit = new Transaction();
                debit.CreatedBy = "Admin";
                debit.DateCreated = DateTime.Now;
                debit.Narration = "Capital Budget";
                debit.Reference = tranId;
                debit.TransactionDate =DateTime.Now;
                debit.TransactionStatusId = 2;
                debit.DebitAccountId = 4;//Bank Account Id No = 4 
                debit.CreditAccountId = 3;//Capital Account Id No = 3
                debit.TransactionTypeId = 1;
                debit.Debit = 0;
                debit.Credit = amount;


                Transaction credit = new Transaction();
                credit.CreatedBy = "Admin";
                credit.DateCreated = DateTime.Now;
                credit.Narration = "Capital Budget";
                credit.Reference = tranId;
                credit.TransactionDate = DateTime.Now;
                credit.DebitAccountId = 4;//Bank Account Id No = 4 
                credit.CreditAccountId = 3;//Capital Account Id No = 3
                credit.TransactionStatusId = 2;
                credit.TransactionTypeId = 1;
                credit.Credit = 0;
                credit.Debit = amount;

                dbAccess.SaveOrUpdate(debit);
                dbAccess.SaveOrUpdate(credit);

                responseObject.Data = "A budget transaction was added successfully";
                responseObject.ResponseMessage = $"Transaction was added successfully!";
                responseObject.ResponseStatus = true;

                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseInfo<string> { ResponseStatus = false, ResponseMessage = ex.Message });
            }
        }

        [HttpGet("CheckBalance")]
        [Authorize(Roles = "Admin,User")]
        public ActionResult CheckBalance()
        {
           
            var responseObject = new ResponseInfo<string>();
            try
            {
                responseObject.Data = dbAccess.GetBalance().ToString();
                responseObject.ResponseMessage = $"Balance generated";
                responseObject.ResponseStatus = true;

                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseInfo<string> { ResponseStatus = false, ResponseMessage = ex.Message });
            }
            
        }

        
    }
}

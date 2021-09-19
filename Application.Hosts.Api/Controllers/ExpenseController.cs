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
    public class ExpenseController : ControllerBase
    {

        private readonly IDbAccess dbAccess;

        //private readonly ILogger logger;

        public ExpenseController(IDbAccess _dbAccess)
        {
            if (_dbAccess == null)
                throw new ArgumentNullException(nameof(_dbAccess));

            dbAccess = _dbAccess;

        }


        /// <summary>
        /// Create an expense transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>

        [HttpPost("CreateExpenseTransaction")]
        [Authorize(Roles = "Admin,User")]
        public ActionResult CreateExpenseTransaction([FromQuery] TransactionModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (transaction.Amount < 0)
                return BadRequest("Invalid Amount");

            var responseObject = new ResponseInfo<TransactionModel>();
            try
            {
                //For Transaction Integrity, Every transaction should have a debit entry and a corresponding credit entry.
                //Accounts here are hard coded for demonstration purposes
                if (dbAccess.GetBalance()-transaction.Amount <= 0)
                {
                    return Ok(new ResponseInfo<string> { ResponseStatus = false, ResponseMessage = "Cannot perform transaction above the available balance of " + dbAccess.GetBalance().ToString() });
                }
                var tranId = Guid.NewGuid().ToString();
                Transaction debit = new Transaction();
                debit.CreatedBy = "Admin";
                debit.DateCreated = DateTime.Now;
                debit.Narration = transaction.Narration;
                debit.Reference = tranId;
                debit.TransactionDate = transaction.TransactionDate;
                debit.TransactionStatusId = 2;
                debit.DebitAccountId = 1;//Expense
                debit.CreditAccountId = 4;//Bank
                debit.TransactionTypeId = 2;
                debit.Credit = 0;
                debit.Debit = transaction.Amount;


                Transaction credit = new Transaction();
                credit.CreatedBy = "Admin";
                credit.DateCreated = DateTime.Now;
                credit.Narration = transaction.Narration;
                credit.Reference = tranId;
                credit.TransactionDate = transaction.TransactionDate;
                credit.DebitAccountId = 2;//Bank
                credit.CreditAccountId = 1;//Expense
                credit.TransactionStatusId = 2;
                credit.TransactionTypeId = 2;
                credit.Debit = 0;
                credit.Credit = transaction.Amount;

                dbAccess.SaveOrUpdate(debit);
                dbAccess.SaveOrUpdate(credit);

                responseObject.Data = transaction;
                responseObject.ResponseMessage = $"Transaction was added successfully!";
                responseObject.ResponseStatus = true;

                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseInfo<string> { ResponseStatus = false, ResponseMessage = ex.Message });
            }
        }

        /// <summary>
        /// Get all transactions
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTransactions")]
        [Authorize(Roles = "Admin,User")]
        public ActionResult GetAllTransactions()
        {
            var responseObject = new ResponseInfo<List<TransactionModel>>();
            try
            {
                List<TransactionModel> transactions = new List<TransactionModel>();
                var trans = dbAccess.GetTransactions().ToList();

                int count = 2;
                foreach (var tran in trans)
                {
                    if (count%2 ==0)
                    {
                        transactions.Add(
                        new TransactionModel()
                        {
                            Amount = tran.Credit + tran.Debit,
                            Reference = tran.Reference,
                            Narration = tran.Narration,
                            TransactionDate = tran.TransactionDate,

                        });
                    }
                    count++;
                }

                responseObject.Data = transactions;
                responseObject.ResponseMessage = $"All transactions loaded";
                responseObject.ResponseStatus = true;

            }
            catch (Exception ex)
            {
                return Ok(new ResponseInfo<string> { ResponseStatus = false, ResponseMessage = ex.Message });
            }


            return Ok(responseObject);
        }

        /// <summary>
        /// Get pending transactions
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPendingTransactions")]
        [Authorize(Roles = "Admin,User")]
        public ActionResult GetPendingTransactions()
        {
            var responseObject = new ResponseInfo<List<TransactionModel>>();
            try
            {
                List<TransactionModel> transactions = new List<TransactionModel>();
                var trans = dbAccess.GetTransactions().Where(a=>a.TransactionStatusId==2).ToList();

                int count = 2;
                foreach (var tran in trans)
                {
                    if (count % 2 == 0)
                    {
                        transactions.Add(
                        new TransactionModel()
                        {
                            Amount = tran.Credit + tran.Debit,
                            Reference = tran.Reference,
                            Narration = tran.Narration,
                            TransactionDate = tran.TransactionDate,

                        });
                    }
                    count++;
                }

                responseObject.Data = transactions;
                responseObject.ResponseMessage = $"All transactions loaded";
                responseObject.ResponseStatus = true;

            }
            catch (Exception ex)
            {
                return Ok(new ResponseInfo<string> { ResponseStatus = false, ResponseMessage = ex.Message });
            }


            return Ok(responseObject);
        }


        /// <summary>
        /// Get rejected transactions
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetRejectedTransactions")]
        [Authorize(Roles = "Admin,User")]
        public ActionResult GetRejectedTransactions()
        {
            var responseObject = new ResponseInfo<List<TransactionModel>>();
            try
            {
                List<TransactionModel> transactions = new List<TransactionModel>();
                var trans = dbAccess.GetTransactions().Where(a => a.TransactionStatusId == 3).ToList();

                int count = 2;
                foreach (var tran in trans)
                {
                    if (count % 2 == 0)
                    {
                        transactions.Add(
                        new TransactionModel()
                        {
                            Amount = tran.Credit + tran.Debit,
                            Reference = tran.Reference,
                            Narration = tran.Narration,
                            TransactionDate = tran.TransactionDate,
                          
                        });
                    }
                    count++;
                }

                responseObject.Data = transactions;
                responseObject.ResponseMessage = $"All transactions loaded";
                responseObject.ResponseStatus = true;

            }
            catch (Exception ex)
            {
                return Ok(new ResponseInfo<string> { ResponseStatus = false, ResponseMessage = ex.Message });
            }


            return Ok(responseObject);
        }


        /// <summary>
        /// Approve a transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPost("ApproveTransaction")]
        [Authorize("Admin")]
        public ActionResult ApproveTransaction([FromBody]TransactionModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (transaction.Amount < 0)
                return BadRequest("Invalid Amount");

            var responseObject = new ResponseInfo<TransactionModel>();
            try
            {
                var transactions = dbAccess.GetTransactions().Where(a => a.Reference == transaction.Reference).ToList();
                if (transactions != null)
                {
                    foreach (var item in transactions)
                    {
                        item.TransactionStatusId = 1;
                        dbAccess.SaveOrUpdate(item);
                    }
                }
               
                responseObject.Data = transaction;
                responseObject.ResponseMessage = $"Transaction was updated successfully!";
                responseObject.ResponseStatus = true;

                return Ok(responseObject);
            }
            catch (Exception ex)
            {
                return Ok(new ResponseInfo<string> { ResponseStatus = false, ResponseMessage = ex.Message });
            }
        }

        /// <summary>
        /// Reject a transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPost("RejectTransaction")]
        [Authorize("Admin")]
        public ActionResult RejectTransaction(TransactionModel transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (transaction.Amount < 0)
                return BadRequest("Invalid Amount");

            var responseObject = new ResponseInfo<TransactionModel>();
            try
            {
                var transactions = dbAccess.GetTransactions().Where(a => a.Reference == transaction.Reference).ToList();
                if (transactions != null)
                {
                    foreach (var item in transactions)
                    {
                        item.TransactionStatusId = 3;
                        dbAccess.SaveOrUpdate(item);
                    }
                }

                responseObject.Data = transaction;
                responseObject.ResponseMessage = $"Transaction was updated successfully!";
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

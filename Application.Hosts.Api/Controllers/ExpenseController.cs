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

    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
      
        private readonly IDbAccess dbAccess;
        public ExpenseController(IDbAccess _dbAccess)
        {
            if (_dbAccess == null)
                throw new ArgumentNullException(nameof(_dbAccess));
            dbAccess = _dbAccess;

        }

        [HttpGet("GetExpenses")]
        [Authorize]
        public string GetExpenses()
        {
            return "Test 1";
        }
    }
}

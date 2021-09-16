using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Contracts.DatabaseSessions
{
    using Application.Domain.Models;

    public interface IDbAccess
    {
        /// <summary>
        /// Returns all SystemUsers
        /// </summary>
        /// <returns></returns>
        IQueryable<SystemUser> GetAllSystemUsers();
    }
}

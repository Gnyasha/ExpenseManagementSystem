using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Contracts.Data
{
    using Application.Domain.Models;

    /// <summary>
    /// This ensures that the SystemUser DAO implements these functions
    /// </summary>
    public interface ISystemUserDao
    {
        IQueryable<SystemUser> GetAllSystemUsers();
        SystemUser SaveOrUpdatePerson(SystemUser person);
    }
}

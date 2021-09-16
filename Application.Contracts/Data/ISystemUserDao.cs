using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Contracts.Data
{
    using Application.Domain.Models;
    using Application.Contracts.Data.Base;
    using Application.Data.Mapping;

    /// <summary>
    /// This ensures that the SystemUser DAO implements these functions
    /// </summary>
    public interface ISystemUserDao : IGenericDao<SystemUser>
    {
        IQueryable<SystemUser> GetAllSystemUsers();
        SystemUser SaveOrUpdatePerson(SystemUser person);
    }
}

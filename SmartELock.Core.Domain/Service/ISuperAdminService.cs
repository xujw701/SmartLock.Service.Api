using SmartELock.Core.Domain.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Service
{
    public interface ISuperAdminService
    {
        Task<int> CreateSuperAdmin(SuperAdminCreateCommand command);
    }
}

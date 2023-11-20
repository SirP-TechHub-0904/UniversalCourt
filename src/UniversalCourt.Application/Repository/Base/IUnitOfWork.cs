using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalCourt.Application.Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
    }
}

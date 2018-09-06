using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottary.Services.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}

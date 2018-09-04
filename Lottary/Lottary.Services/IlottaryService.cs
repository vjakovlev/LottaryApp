using Lottary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottary.Services
{
    public interface IlottaryService
    {
        AwardModel CheckCode(UserCodeModel userCode);
    }
}
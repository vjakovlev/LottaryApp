using Lottary.Data;
using Lottary.DataModel;
using Lottary.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottary.Services
{
    public class LottaryService : IlottaryService
    {
        public AwardModel CheckCode(UserCodeModel userCodeModel)
        {
            var _codeRepository = new Repository<Code>(new DbContext("LottaryDb"));

            var code = _codeRepository.GetAll().FirstOrDefault(x => x.CodeValue == userCodeModel.Code.CodeValue);

            if (code == null)
            {
                throw new ApplicationException("Invalid Code.");
            }

            if (code.IsUsed)
            {
                throw new ApplicationException("Code is used.");
            }

            var userCode = new UserCode
            {
                Code = code,
                Email = userCodeModel.Email,
                FirstName = userCodeModel.Email,
                LastName = userCodeModel.LastName,
                SentAt = DateTime.Now
            };

            var _userCodeRepository = new Repository<UserCode>(
                new DbContext("LottaryDb"));
            _userCodeRepository.Insert(userCode);

            Award award = new Award()
            {
                RuffleType = (byte)RuffledType.Immediate,
                AwardName = "Another 0.5 bottle",
                Quantity = 1
            };

            if (code.IsWinning)
            {
                var userCodeAward = new UserCodeAward
                {
                    Award = award,
                    UserCode = userCode,
                    WonAt = DateTime.Now
                };

                var _userCodeAwardRepository = new Repository<UserCodeAward>(new DbContext("LottaryDb"));
                _userCodeAwardRepository.Insert(userCodeAward);
            }

            return new AwardModel()
            {
                AwardName = award.AwardName,
                AwardDescription = award.AwardDescription
            };
        }
    }
}

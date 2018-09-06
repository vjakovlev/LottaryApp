using Lottary.Data;
using Lottary.DataModel;
using Lottary.Mappers;
using Lottary.Services.UoW;
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
        private readonly DbContext _dbContext;
        private readonly IRepository<Code> _codeRepository;
        private readonly IRepository<Award> _awardRepository;
        private readonly IRepository<UserCode> _userCodeRepository;
        private readonly IRepository<UserCodeAward> _userCodeAwardRepository;

        public LottaryService(IRepository<Code> codeRepository,
            IRepository<Award> awardRepository,
            IRepository<UserCode> userCodeRepository,
            IRepository<UserCodeAward> userCodeAwardRepository,
            DbContext dbContext)
        {
            _codeRepository = codeRepository;
            _awardRepository = awardRepository;
            _userCodeRepository = userCodeRepository;
            _userCodeAwardRepository = userCodeAwardRepository;
            _dbContext = dbContext;
        }

        public AwardModel CheckCode(UserCodeModel userCodeModel)
        {
            using (var uow = new UnitOfWork(_dbContext)) 
            {
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
                    FirstName = userCodeModel.FirstName,
                    LastName = userCodeModel.LastName,
                    SentAt = DateTime.Now
                };

                _userCodeRepository.Insert(userCode);

                Award award = null;

                if (code.IsWinning)
                {
                    award = GetRAndomAward(RuffledType.Immediate);

                    var userCodeAward = new UserCodeAward
                    {
                         Award = award,
                         UserCode = userCode,
                         WonAt = DateTime.Now
                    };

                    _userCodeAwardRepository.Insert(userCodeAward);
                }

                code.IsUsed = true;

                uow.Commit();

                return award?.Map<Award, AwardModel>();
            }

        }

        private Award GetRAndomAward(RuffledType type)
        {
            var awards = _awardRepository.GetAll().Where(x => x.RuffleType == (byte)type).ToList();

            var awardedAwards = _userCodeAwardRepository
                .GetAll()
                .Where(x => x.Award.RuffleType == (byte)type)
                .Select(x => x.Award)
                .GroupBy(x => x.Id)
                .ToList();

            var availableAwards = new List<Award>();

            foreach (var award in awards)
            {
                var numberOfAwards = awardedAwards
                    .FirstOrDefault(x => x.Key == award.Id)?.Count() ?? 0;
                var awardsLeft = award.Quantity - numberOfAwards;

                availableAwards.AddRange(Enumerable.Repeat(award, awardsLeft));
            }

            if (availableAwards.Count == 0)
            {
                throw new ApplicationException("We are out of rewords. Sorry!");
            }

            var rnd = new Random();
            var randomAwardIndex = rnd.Next(0, availableAwards.Count);
            return availableAwards[randomAwardIndex];
        }
    }
}

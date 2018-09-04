using Lottary.Services;
using Lottary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lottary.Web.Controllers
{
    public class LottaryController : ApiController
    {
        private readonly IlottaryService _lottaryService;

        public LottaryController(IlottaryService lottaryService)
        {
            _lottaryService = lottaryService;
        }

        [HttpPost]
        public AwardModel SubmitCode([FromBody] UserCodeModel userCodeModel)
        {
            return _lottaryService.CheckCode(userCodeModel);
        }


    }
}

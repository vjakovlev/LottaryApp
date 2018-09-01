using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace Lottary.DataModel.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LottaryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LottaryContext context)
        {
            var codes = new List<Code>
            {
                new Code
                {
                    CodeValue = "CC8899",
                    IsWinning = true
                },

                new Code
                {
                    CodeValue = "CC7799",
                    IsWinning = false
                },

                new Code
                {
                    CodeValue = "CC6699",
                    IsWinning = false
                },

                new Code
                {
                    CodeValue = "CC5599",
                    IsWinning = true
                },
            };

            context.Codes.AddRange(codes);

            var awards = new List<Award>
            {
                new Award
                {
                    AwardName = "beer",
                    AwardDescription = "You won a beer",
                    Quantity = 100,
                    RuffleType = (byte) RuffledType.Immediate
                },

                new Award
                {
                    AwardName = "t-shirt",
                    AwardDescription = "You won a t-shirt",
                    Quantity = 50,
                    RuffleType = (byte) RuffledType.Immediate
                },

                new Award
                {
                    AwardName = "iPhoneX",
                    AwardDescription = "You won an iPhoneX",
                    Quantity = 2,
                    RuffleType = (byte) RuffledType.PerDay
                },

                new Award
                {
                    AwardName = "VW Polo",
                    AwardDescription = "You won a VW Polo",
                    Quantity = 1,
                    RuffleType = (byte) RuffledType.Final
                },
            };

            context.Awards.AddRange(awards);

            context.SaveChanges();
        }
    }
}

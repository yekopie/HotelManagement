using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {

        public static IOutcome Run(params IOutcome[] outcomes)
        {
            foreach (var outcome in outcomes)
            {
                if (!outcome.IsSuccess)
                {
                    return outcome;
                }

            }

            return Outcomes.Success;
        }

        public static bool IsSuccessfull(this IOutcome outcome)
        {
            return (outcome != null && outcome.IsSuccess) ? true : false;
        }

    }
}

using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {

        public static IOutcome? Run(params IOutcome[] outcomes)
        {
            if (outcomes != null)
            {
                foreach (var outcome in outcomes)
                {
                    if(outcome == null) continue;
                    if(!outcome.IsSuccess)
                    {
                        return outcome;
                    }

                }
            }

            return null;
        }

        public static bool IsSuccessfull(this IOutcome? outcome)   
        { 
            return (outcome != null && outcome.IsSuccess) ? true : false;
        }

    }
}

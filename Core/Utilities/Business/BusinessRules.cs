using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {

        public IOutcome? Run(params IOutcome[] outcomes)
        {
            if (outcomes != null)
            {
                foreach (var outcome in outcomes)
                {
                    if(!outcome.IsSuccess)
                    {
                        return outcome;
                    }

                }
            }

            return null;
        }

    }
}

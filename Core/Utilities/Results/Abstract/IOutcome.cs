using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Abstract
{
    public interface IOutcome
    {
        string Message { get; }
        bool IsSuccess { get; }
    }
}

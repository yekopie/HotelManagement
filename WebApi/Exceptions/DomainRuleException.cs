
namespace WebApi.Exceptions
{
    public class DomainRuleException : BaseException
    {
        public DomainRuleException(string message, IReadOnlyList<string>? errors = null
            ) : base(message, "DOMAIN_RULE_VIOLATION", StatusCodes.Status403Forbidden, errors)
        {
        }
    }
}

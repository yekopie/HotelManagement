
namespace Core.Utilities.Exceptions
{
    public class DomainRuleException : BaseException
    {
        public DomainRuleException(string message, IReadOnlyList<string>? errors = null) 
            : base(message, "DOMAIN_RULE_VIOLATION", 403, errors)
        {
        }

        public DomainRuleException(string message) 
            : base(message, "DOMAIN_RULE_VIOLATION", 403)
        {
        }
    }
}

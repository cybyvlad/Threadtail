using MessageProcessor.Library.Rules;

namespace MessageProcessor.Library
{
    public interface IRule
    {
        void Execute(IRuleInput ruleInput, IRuleContext context);
    }
}
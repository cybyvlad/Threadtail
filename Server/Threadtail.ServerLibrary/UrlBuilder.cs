namespace Threadtail.ServerLibrary
{
    public class UrlBuilder : IUrlBuilder
    {
        private readonly IJavaScriptTimeCalculator _javaScriptTimeCalculator;

        public UrlBuilder(IJavaScriptTimeCalculator javaScriptTimeCalculator)
        {
            _javaScriptTimeCalculator = javaScriptTimeCalculator;
        }

        #region IUrlBuilder Members

        public string BuildUrl(IThreadtailHttpContext context)
        {
            return context.Url + AddVariable(1, "Browser", context.BrowserName);
        }

        #endregion

        private string AddVariable(int variableIndex, string variableName, string variableValue)
        {
            return string.Format("&enx{0}={1}&evx{0}={2}&tx{0}={3}", variableIndex, variableName, variableValue,
                                 _javaScriptTimeCalculator.GetCurrentDateAsLong());
        }
    }
}
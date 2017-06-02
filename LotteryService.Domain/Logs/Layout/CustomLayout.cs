
namespace LotteryService.Domain.Logs.Layout
{
    public class CustomLayout : log4net.Layout.PatternLayout
    {
        public CustomLayout()
        {
            this.AddConverter("property", typeof(CustomPatternConverter));
        }
    }
}

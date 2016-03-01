using RazorEngine.Templating;
using ReportUnit.Properties;

namespace ReportUnit.Templates
{
    public class Reports
    {
        public static string GetSource()
        {
            return System.Text.Encoding.UTF8.GetString(Resources.Reports);//.Replace("\r\n", "").Replace("\t", "").Replace("    ", "");
        }
    }
}

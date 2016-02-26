using RazorEngine.Templating;

namespace ReportUnit.Templates
{
    public class Reports
    {
        public static string GetSource()
        {
            return System.IO.File.ReadAllText(@"C:\cygwin64\home\Admin\reportunit\ReportUnit\Templates\Reports.cshtml").Replace("\r\n", "").Replace("\t", "").Replace("    ", "");
        }
    }
}

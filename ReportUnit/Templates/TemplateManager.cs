using ReportUnit.Properties;

namespace ReportUnit.Templates
{
    public class TemplateManager
    {
        public static string GetReportsTemplate()
        {
            return GetEncodedResource(Resources.Reports);
        }

        public static string GetTimeAnalyticsTemplate()
        {
            return GetEncodedResource(Resources.TimeAnalytics);
        }

        private static string GetEncodedResource(byte[] resource)
        {
            return System.Text.Encoding.UTF8.GetString(resource);
        }
    }
}

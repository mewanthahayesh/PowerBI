using PowerBIService.Common;

namespace ClientCommon.Contract
{
    public class EmbedReportRequest
    {
        public string ReportId { get; set; }
        public string WorkSpaceId { get; set; }
        public UserData Credential { get; set; }
        public string EmbedReportUrl { get; set; }
        public EmbededReportDataSetParam[] ParaMeters { get; set; }
        public string EmbedUserName { get; set; }
        public string EmbedRoles { get; set; }
    }
}
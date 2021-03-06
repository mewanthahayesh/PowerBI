

using PowerBIService.Common;

namespace ClientCommon.Contract
{
    public class GroupCreateRequest
    {
        public string GroupName { get; set; }
        public UserData Credential { get; set; }
        
        public MembersRights[] Members { get; set; }
    }
}
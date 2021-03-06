using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bifrost.Extensions;
using ClientCommon.Contract;
using Microsoft.AspNetCore.Mvc;
using PowerBIService.Common;
using PowerBIService.Services.Interfaces;
using WebClientDemo.Models;

namespace PowerBIWebApi.Controllers
{
    public class EmbedReportApiController : ControllerBase
    {
        protected readonly IPowerService _powerService;
     
        public EmbedReportApiController(IPowerService powerService)
        {
            _powerService = powerService;
        }
        /// <summary>
        /// Get All Groups within Service principles
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/reports/GetWorkSpaceReports/{groupId}", Name = "WorkspaceAllReports")]
        public async Task<IEnumerable<PowerReport>> GetWorkSpaceReports(WorkSpaceRequestVM workSpaceRequestVm)
        {
            var list = new List<PowerReport>();

            var request = new GetReportRequest {Credential =new UserData
            {
                TenantId = workSpaceRequestVm.Credential.TenantId,
                SecretId = workSpaceRequestVm.Credential.SecretId,
                ApplicationId = workSpaceRequestVm.Credential.ApplicationId
                
            }, WorkSpaceId = workSpaceRequestVm.GroupId};
            var Result =  await _powerService.GetAllReportInWorkSpace(request);
            Result.ForEach(s =>
            {
                list.Add(new PowerReport { Id = s.Id, Name = s.Name });
            });
            return list.ToArray();
        }
        
        
        [HttpPost]
        [Route("api/reports/CreatePowerBiGroup", Name = "CreatePowerBiGroup")]
        public async Task<bool> CreatePowerBiGroup(GroupCreateRequestVM groupCreateRequest)
        {
            var request = new GroupCreateRequest {Credential =new UserData
            {
                TenantId = groupCreateRequest.Credential.TenantId,
                SecretId = groupCreateRequest.Credential.SecretId,
                ApplicationId = groupCreateRequest.Credential.ApplicationId
                
            },
            GroupName = groupCreateRequest.GroupName,
            Members = groupCreateRequest.Members.Select(s =>
            {
                var membersRights = new MembersRights
                {
                    MemberEmail = s.MemberEmail,
                    GroupUserAccessRight = s.GroupUserAccessRight
                };
                return membersRights;
                
            }).ToArray()};
            var Result =  await _powerService.CreateGroup(request);
            return Result;
        }
        
        [HttpPost]
        [Route("api/reports/AssignPowerBiGroupUsers", Name = "AssignPowerBiGroupUsers")]
        public async Task<bool> AssignPowerBiGroupUsers(GroupMemberAssignRequestVM groupMemberAssignRequest)
        {
            var request = new GroupMemberAssignRequest {Credential =new UserData
                {
                    TenantId = groupMemberAssignRequest.Credential.TenantId,
                    SecretId = groupMemberAssignRequest.Credential.SecretId,
                    ApplicationId = groupMemberAssignRequest.Credential.ApplicationId
                
                },
                GroupId = groupMemberAssignRequest.GroupId,
                Members = groupMemberAssignRequest.Members.Select(s =>
                {
                    var membersRights = new MembersRights
                    {
                        MemberEmail = s.MemberEmail,
                        GroupUserAccessRight = s.GroupUserAccessRight
                    };
                    return membersRights;
                
                }).ToArray()};
            var Result =  await _powerService.AssignUsersToGroup(request);
            return Result;
        }
        /// <summary>
        /// Clone Report from report repository
        /// </summary>
        /// <param name="cloneReport"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/reports/CloneReport", Name = "CloneReport")]
        public async Task<IEnumerable<CloneReportResponseVM>> CloneReport(CloneReportRequestVM cloneReport)
        {

            var cloneReportRequest = new CloneReportRequest
            {
                
                ClientWorkSpaceId = cloneReport.ClientWorkSpaceId,
                ParentWorkSpaceId = cloneReport.ParentWorkSpaceId,
                CloneReports = cloneReport.CloneReports.Select(w => new CloneReport { CloneReportName = w.CloneReportName, ParentReportId = w.ParentReportId, WebApiEndPoint = w.WebApiEndPoint }).ToArray()

            };
            cloneReportRequest.Credential.SecretId = cloneReport.Credential.SecretId;
            cloneReportRequest.Credential.TenantId = cloneReport.Credential.TenantId;
            cloneReportRequest.Credential.ApplicationId = cloneReport.Credential.ApplicationId;
            
            var result =  await _powerService.CloneReports(cloneReportRequest);
            var responseData = result;

            var responseList = new List<CloneReportResponseVM>();

            responseData.ForEach(s =>
            {
                responseList.Add(new CloneReportResponseVM
                {
                    CloneReportName = s.CloneReportName,
                    ParentReportName = s.ParentReportName,
                    Success = s.Success

                });
            });
            return responseList.ToArray();
        }
        /// <summary>
        /// Report Embedding token provider
        /// </summary>
        /// <param name="embedReportRequest"></param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("api/reports/EmbedReport", Name = "EmbedReport")]
        public async Task<EmbedConfig> EmbedReport(EmbedReportRequestVM embedReportRequest)
        {
            var embedRequest=new EmbedReportRequest
            {
                Credential = new UserData
                {
                    SecretId = embedReportRequest.Credential.SecretId,
                    TenantId = embedReportRequest.Credential.TenantId,
                    ApplicationId = embedReportRequest.Credential.ApplicationId
                },
                EmbedRoles = embedReportRequest.EmbedRoles,
                ReportId = embedReportRequest.ReportId,
                EmbedUserName = embedReportRequest.EmbedUserName,
                WorkSpaceId = embedReportRequest.WorkSpaceId,
                EmbedReportUrl = embedReportRequest.EmbedReportUrl,
                ParaMeters = embedReportRequest.ParaMeters.Select(s=> new EmbededReportDataSetParam{ParaType = s.ParaType,ParamName = s.ParamName,ParamValue = s.ParamValue}).ToArray()
            };
            var result =  await _powerService.ClientEmbedReport(embedRequest);
            var responseData = result;
            
            return responseData;
        }
        /// <summary>
        /// Get All Groups
        /// </summary>
        /// <param name="credentialVm"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/reports/AllGroups", Name = "GetAllGroups")]
        public async Task<IEnumerable<GroupsVM>> GetAllGroups(CredentialVM credentialVm)
        {
            var list = new List<GroupsVM>();

            var Credential = new UserData
            {
                SecretId = credentialVm.SecretId,
                TenantId = credentialVm.TenantId,
                ApplicationId = credentialVm.ApplicationId
            };
            
            var Result =  await _powerService.GetAllGroups(Credential);
            Result.ForEach(s =>
            {
                list.Add(new GroupsVM { GroupId = s.Id, GroupName = s.Name });
            });
            return list.ToArray();
        }
    }
}
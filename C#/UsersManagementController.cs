using Dev.Models.Responses;
using Dev.Models.ViewModels;
using Dev.Services.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dev.Web.Controllers.Api
{
    [AllowAnonymous]
    [RoutePrefix("api/usersmanagement")]
    public class UsersManagementApiController : ApiController
    {
        private IUsersManagementService _usersManagementService;
        public UsersManagementApiController(IUsersManagementService usersManagementService)
        {
            _usersManagementService = usersManagementService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage UsersManagementGetAll()
        {
            try
            {
                ItemsResponse<UsersManagementViewModel> resp = new ItemsResponse<UsersManagementViewModel>();
                resp.Items = _usersManagementService.UsersManagementGetAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("insertuserroles/{UserId:int}/{RoleId:int}")]
        public HttpResponseMessage InsertUserRoles(int UserId, int RoleId)
        {
            try
            {
                _usersManagementService.InsertUserRoles(UserId, RoleId);
                SuccessResponse resp = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [Route("deleteuserroles/{UserId:int}/{RoleId:int}")]
        public HttpResponseMessage DeleteUserRoles(int UserId, int RoleId)
        {
            try
            {
                _usersManagementService.DeleteUserRoles(UserId, RoleId);
                SuccessResponse resp = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
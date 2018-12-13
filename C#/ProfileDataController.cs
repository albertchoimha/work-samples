using Dev.Models.Requests;
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
    [RoutePrefix("api/profiledata")]
    public class ProfileDataApiController : ApiController
    {
        private IProfileDataService _profileDataService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ProfileDataApiController(IProfileDataService profileDataService)
        {
            _profileDataService = profileDataService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int Id)
        {
            try
            {
                ItemResponse<ProfileDataViewModel> resp = new ItemResponse<ProfileDataViewModel>();
                resp.Item = _profileDataService.GetById(Id);
                log.Info("ProfileData GetById Successful");
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                log.Error("ProfileData GetById Error", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("usersupdate/{id:int}")]
        public HttpResponseMessage UpdateUsers(ProfileDataUsersUpdateRequest model)
        {
            try
            {
                _profileDataService.UpdateUsers(model);
                SuccessResponse resp = new SuccessResponse();
                log.Info("ProfileData Update User Success");
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                log.Error("ProfileData Update User Error", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("personupdate/{id:int}")]
        public HttpResponseMessage UpdatePerson(ProfileDataPersonUpdateRequest model)
        {
            try
            {
                _profileDataService.UpdatePerson(model);
                SuccessResponse resp = new SuccessResponse();
                log.Info("ProfileData Update Person Success");
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                log.Error("ProfileData Update Person Error", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("profileupdate/{id:int}")]
        public HttpResponseMessage UpdateProfile(ProfileDataProfileUpdateRequest model)
        {
            try
            {
                _profileDataService.UpdateProfile(model);
                SuccessResponse resp = new SuccessResponse();
                log.Info("ProfileData Update Profile Success");
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                log.Error("ProfileData Update Profile Error", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("phonetype")]
        public HttpResponseMessage PhoneTypeGetAll()
        {
            try
            {
                ItemsResponse<PhoneTypeViewModel> resp = new ItemsResponse<PhoneTypeViewModel>();
                resp.Items = _profileDataService.PhoneTypeGetAll();
                log.Info("ProfileData GetAll PhoneType Success");
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                log.Error("ProfileData GetAll PhoneType Error", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("telephone")]
        public HttpResponseMessage TelephoneInsert(TelephoneAddRequest model)
        {
            try
            {
                int Id = _profileDataService.InsertTelephone(model);
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = Id;
                log.Info("ProfileData Insert Telephone Success");
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                log.Error("ProfileData Insert Telephone Error", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("personphone")]
        public HttpResponseMessage PersonPhoneInsert(PersonPhoneAddRequest model)
        {
            try
            {
                int Id = _profileDataService.InsertPersonPhone(model);
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = Id;
                log.Info("ProfileData Insert PersonPhone Success");
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                log.Error("ProfileData Insert PersonPhone Error", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        [Route("deletepersonphone/{id:int}")]
        public HttpResponseMessage DeletePersonPhone(int Id)
        {
            try
            {
                _profileDataService.DeletePersonPhone(Id);
                SuccessResponse resp = new SuccessResponse();
                log.Info("ProfileData Delete PersonPhone Success");
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                log.Error("ProfileData Delete PesonPhone Error", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("profileimage")]
        public HttpResponseMessage ProfileImageInsert(ProfileImageAddRequest model)
        {
            try
            {
                int Id = _profileDataService.InsertProfileImage(model);
                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = Id;
                log.Info("ProfileData Insert ProfileImage Success");
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                log.Error("ProfileData Insert ProfileImage Error", ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
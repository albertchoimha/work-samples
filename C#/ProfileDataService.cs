using Dev.Data;
using Dev.Data.Providers;
using Dev.Models.Domain;
using Dev.Models.Requests;
using Dev.Models.ViewModels;
using Dev.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dev.Services.Profile
{
    public class ProfileDataService : IProfileDataService
    {
        private IDataProvider _dataProvider;
        private IConfigService _configService;
        public string BaseUrl { get; set; }

        public ProfileDataService(IDataProvider dataProvider, IConfigService configService)
        {
            _dataProvider = dataProvider;
            _configService = configService;
            BaseUrl = _configService.SelectByKey("image_base_url").ConfigValue;
        }

        public ProfileTelephoneModel MapPDViewModel(IDataReader reader, int index)
        {
            ProfileTelephoneModel model = new ProfileTelephoneModel
            {
                UserId = reader.GetSafeInt32(index++),
                Email = reader.GetSafeString(index++),
                UserName = reader.GetSafeString(index++),
                FirstName = reader.GetSafeString(index++),
                MiddleInitial = reader.GetSafeString(index++),
                LastName = reader.GetSafeString(index++),
                Gender = reader.GetSafeString(index++),
                Bio = reader.GetSafeString(index++),
                Title = reader.GetSafeString(index++),
                BasePath = reader.GetSafeString(index++),
                SystemFileName = reader.GetSafeString(index++),
                PhoneId = reader.GetSafeInt32(index++),
                PhoneNumber = reader.GetSafeString(index++),
                PhoneExtension = reader.GetSafeString(index++),
                PhoneType = reader.GetSafeInt32(index++),
                DisplayName = reader.GetSafeString(index++),
                BaseUrl = BaseUrl,
                ImageId = reader.GetSafeInt32(index++)
            };
            return model;
        }

        public PhoneTypeViewModel MapPTViewModel(IDataReader reader, int index)
        {
            PhoneTypeViewModel model = new PhoneTypeViewModel
            {
                Id = reader.GetSafeInt32(index++),
                DisplayName = reader.GetSafeString(index++),
                Description = reader.GetSafeString(index++)
            };
            return model;
        }

        public ProfileDataViewModel GetById(int Id)
        {
            ProfileTelephoneModel model = new ProfileTelephoneModel();
            List<ProfileTelephoneModel> myList = new List<ProfileTelephoneModel>();
            _dataProvider.ExecuteCmd(
                "ProfileData_GetById",
                inputParamMapper: delegate (SqlParameterCollection parmList)
                {
                    parmList.AddWithValue("@Id", Id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    int index = 0;
                    model = MapPDViewModel(reader, index);
                    myList.Add(model);
                });

            ProfileDataViewModel profile = new ProfileDataViewModel();
            foreach (var ptm in myList)
            {
                profile.UserId = ptm.UserId;
                profile.Email = ptm.Email;
                profile.UserName = ptm.UserName;
                profile.FirstName = ptm.FirstName;
                profile.MiddleInitial = ptm.MiddleInitial;
                profile.LastName = ptm.LastName;
                profile.Gender = ptm.Gender;
                profile.Bio = ptm.Bio;
                profile.Title = ptm.Title;
                profile.BasePath = ptm.BasePath;
                profile.SystemFileName = ptm.SystemFileName;
                profile.BaseUrl = BaseUrl;
                profile.ImageId = ptm.ImageId;

                TelephoneDomainModel telephone = new TelephoneDomainModel();
                telephone.Id = ptm.PhoneId;
                telephone.PhoneNumber = ptm.PhoneNumber;
                telephone.Extension = ptm.PhoneExtension;
                telephone.PhoneType = ptm.PhoneType;
                telephone.DisplayName = ptm.DisplayName;
                profile.Telephones.Add(telephone);
            }
            return profile;
        }

        public int UpdateUsers(ProfileDataUsersUpdateRequest model)
        {
            int Id = 0;
            _dataProvider.ExecuteNonQuery(
                "ProfileDataUsers_Update",
                inputParamMapper: delegate (SqlParameterCollection paramList)
                {
                    paramList.AddWithValue("@Id", model.UserId);
                    paramList.AddWithValue("@Email", model.Email);
                    paramList.AddWithValue("@UserName", model.UserName);
                });
            return Id;
        }

        public int UpdatePerson(ProfileDataPersonUpdateRequest model)
        {
            int Id = 0;
            _dataProvider.ExecuteNonQuery(
                "ProfileDataPerson_Update",
                inputParamMapper: delegate (SqlParameterCollection paramList)
                {
                    paramList.AddWithValue("@UserId", model.UserId);
                    paramList.AddWithValue("@FirstName", model.FirstName);
                    paramList.AddWithValue("@MiddleInitial", model.MiddleInitial);
                    paramList.AddWithValue("@LastName", model.LastName);
                    paramList.AddWithValue("@ModifiedBy", model.ModifiedBy);
                    paramList.AddWithValue("@Gender", model.Gender);
                });
            return Id;
        }

        public int UpdateProfile(ProfileDataProfileUpdateRequest model)
        {
            int Id = 0;
            _dataProvider.ExecuteNonQuery(
                "ProfileDataProfile_Update",
                inputParamMapper: delegate (SqlParameterCollection paramList)
                {
                    paramList.AddWithValue("@UserId", model.UserId);
                    paramList.AddWithValue("@Title", model.Title);
                    paramList.AddWithValue("@Bio", model.Bio);
                    paramList.AddWithValue("@ModifiedBy", model.ModifiedBy);
                });
            return Id;
        }

        public List<PhoneTypeViewModel> PhoneTypeGetAll()
        {
            List<PhoneTypeViewModel> myList = new List<PhoneTypeViewModel>();
            _dataProvider.ExecuteCmd(
                "PhoneType_GetAll",
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    PhoneTypeViewModel model = new PhoneTypeViewModel();
                    int index = 0;
                    model = MapPTViewModel(reader, index);
                    myList.Add(model);
                });
            return myList;
        }

        public int InsertTelephone(TelephoneAddRequest model)
        {
            int Id = 0;
            _dataProvider.ExecuteNonQuery(
                "Telephone_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramList)
                {
                    SqlParameter parm = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                    paramList.Add(parm);
                    paramList.AddWithValue("@PhoneNumber", model.PhoneNumber);
                    paramList.AddWithValue("@Extension", model.Extension);
                    paramList.AddWithValue("@PhoneType", model.PhoneType);
                    paramList.AddWithValue("@ModifiedBy", model.ModifiedBy);
                },
                returnParameters: delegate (SqlParameterCollection paramList)
                {
                    Id = (int)paramList["@Id"].Value;
                });
            return Id;
        }

        public int InsertPersonPhone(PersonPhoneAddRequest model)
        {
            int UserId = 0;
            _dataProvider.ExecuteNonQuery(
                "PersonPhone_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramList)
                {
                    paramList.AddWithValue("@UserId", model.UserId);
                    paramList.AddWithValue("@PhoneId", model.PhoneId);
                },
                returnParameters: delegate (SqlParameterCollection paramList)
                {
                    UserId = (int)paramList["@UserId"].Value;
                });
            return UserId;
        }

        public void DeletePersonPhone(int Id)
        {
            _dataProvider.ExecuteNonQuery(
                "PersonPhone_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramList)
                {
                    paramList.AddWithValue("@PhoneId", Id);
                });
        }

        public int InsertProfileImage(ProfileImageAddRequest model)
        {
            int UserId = 0;
            _dataProvider.ExecuteNonQuery(
                "ProfileImage_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramList)
                {
                    paramList.AddWithValue("@UserId", model.UserId);
                    paramList.AddWithValue("@FileStorageId", model.FileStorageId);
                },
                returnParameters: delegate (SqlParameterCollection paramList)
                {
                    UserId = (int)paramList["@UserId"].Value;
                });
            return UserId;
        }
    }
}


using Dev.Data;
using Dev.Data.Providers;
using Dev.Models.Domain;
using Dev.Models.ViewModels;
using Dev.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dev.Services.UsersManagement
{
    public class UsersManagementService : IUsersManagementService
    {
        private IDataProvider _dataProvider;

        public UsersManagementService(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public static UsersManagementViewModel MapUMViewModel(IDataReader reader, int index)
        {
            UsersManagementViewModel model = new UsersManagementViewModel
            {
                UserId = reader.GetSafeInt32(index++),
                FirstName = reader.GetSafeString(index++),
                MiddleInitial = reader.GetSafeString(index++),
                LastName = reader.GetSafeString(index++),
                Email = reader.GetSafeString(index++)
            };
            return model;
        }

        public static UsersManagementRoleModel MapURDomainModel(IDataReader reader, int index)
        {
            UsersManagementRoleModel model = new UsersManagementRoleModel
            {
                UserId = reader.GetSafeInt32(index++),
                RoleId = reader.GetSafeInt32(index++),
                Role = reader.GetSafeString(index++),
                HasRole = reader.GetSafeInt32(index++)
            };
            return model;
        }

        public List<UsersManagementViewModel> UsersManagementGetAll()
        {
            List<UsersManagementViewModel> myList = new List<UsersManagementViewModel>();
            _dataProvider.ExecuteCmd(
                "UsersManagement_GetAll",
                inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    UsersManagementViewModel model = new UsersManagementViewModel();
                    int index = 0;
                    model = MapUMViewModel(reader, index);
                    myList.Add(model);
                });
            foreach (var um in myList)
            {
                Dictionary<string, UsersManagementRoleModel> userRoleDictionary = new Dictionary<string, UsersManagementRoleModel>();
                _dataProvider.ExecuteCmd(
                    "UsersManagement_GetUserRoles",
                    inputParamMapper: delegate (SqlParameterCollection paramList)
                    {
                        paramList.AddWithValue("@UserId", um.UserId);
                    },
                    singleRecordMapper: delegate (IDataReader reader, short set)
                    {
                        int index = 0;
                        UsersManagementRoleModel ur = MapURDomainModel(reader, index);
                        userRoleDictionary.Add(ur.Role, ur);
                    });
                um.Roles = new UsersManagementDomainModel()
                {
                    SystemAdmin = userRoleDictionary["SystemAdmin"],
                    SystemImplementer = userRoleDictionary["SystemImplementer"],
                    OrganizationAdmin = userRoleDictionary["OrganizationAdmin"],
                    FundingSourceAdmin = userRoleDictionary["FundingSourceAdmin"],
                    SchoolNgoAdmin = userRoleDictionary["SchoolNgoAdmin"],
                    FundingSourceDirector = userRoleDictionary["FundingSourceDirector"],
                    SchoolNgoDirector = userRoleDictionary["SchoolNgoDirector"],
                    OrganizationCaseManager = userRoleDictionary["OrganizationCaseManager"],
                    FundingSourceCaseManager = userRoleDictionary["FundingSourceCaseManager"],
                    SchoolNgoCaseManager = userRoleDictionary["SchoolNgoCaseManager"],
                    ClientUser = userRoleDictionary["ClientUser"],
                };
            }
            return myList;
        }

        public void InsertUserRoles(int UserId, int RoleId)
        {
            _dataProvider.ExecuteNonQuery(
                "UserRoles_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramList)
                {
                    paramList.AddWithValue("@UserId", UserId);
                    paramList.AddWithValue("@RoleId", RoleId);
                });
        }

        public void DeleteUserRoles(int UserId, int RoleId)
        {
            _dataProvider.ExecuteNonQuery(
                "UserRoles_Delete",
                inputParamMapper: delegate (SqlParameterCollection paramList)
                {
                    paramList.AddWithValue("@UserId", UserId);
                    paramList.AddWithValue("@RoleId", RoleId);
                });
        }
    }
}

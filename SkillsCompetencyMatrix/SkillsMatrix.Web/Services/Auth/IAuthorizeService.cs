using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillsMatrix.Web.Services.Auth
{
    public interface IAuthorizeService
    {
        Task Login(LoginModel loginParameters);
        Task Register(RegisterModel registerParameters);
        Task Logout();
        Task<UserInfo> GetUserInfo();
    }
}

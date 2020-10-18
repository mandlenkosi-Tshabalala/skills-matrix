using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace SkillsMatrix.Web.Services.Auth
{
    public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
    {
        private UserInfo _userInfoCache;
        private readonly IAuthorizeService _authorizeService;

        public IdentityAuthenticationStateProvider(IAuthorizeService authorizeService)
        {
            this._authorizeService = authorizeService;
        }

        public async Task Login(LoginModel loginParameters)
        {
            await _authorizeService.Login(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Register(RegisterModel registerParameters)
        {
            await _authorizeService.Register(registerParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            await _authorizeService.Logout();
            _userInfoCache = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<UserInfo> GetUserInfo()
        {
            if (_userInfoCache != null && _userInfoCache.IsAuthenticated) return _userInfoCache;
            _userInfoCache = await _authorizeService.GetUserInfo();
            return _userInfoCache;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetUserInfo();
                if (userInfo.IsAuthenticated)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, _userInfoCache.UserName) }.Concat(_userInfoCache.ExposedClaims.Select(c => new Claim(c.Key, c.Value)));
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));

        }
    }
}

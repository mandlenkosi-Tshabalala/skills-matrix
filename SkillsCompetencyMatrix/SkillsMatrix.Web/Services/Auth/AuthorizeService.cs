using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace SkillsMatrix.Web.Services.Auth
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly HttpClient _httpClient;

        public AuthorizeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Login(LoginModel loginParameters)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Authorize/Login", loginParameters);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task Logout()
        {
            var result = await _httpClient.PostAsync("api/Authorize/Logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterModel registerParameters)
        {
            //var stringContent = new StringContent(JsonSerializer.Serialize(registerParameters), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync("api/Authorize/Register", registerParameters);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<UserInfo> GetUserInfo()
        {
            var result = await _httpClient.GetFromJsonAsync<UserInfo>("api/Authorize/UserInfo");
            return result;
        }
    }
}

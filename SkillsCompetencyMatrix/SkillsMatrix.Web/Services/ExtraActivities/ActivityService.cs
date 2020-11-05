using SkillsMatrix.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SkillsMatrix.Web.Services
{
    public class ActivityService : IActivityService
    {

        public readonly HttpClient httpClient;

        public ActivityService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<UserActivities> Create(UserActivities person)
        {
            return await httpClient.PostJsonAsync<UserActivities>($"api/UserActivities", person);
        }

        public async Task<UserActivities> Get(int Id)
        {
            return await httpClient.GetJsonAsync<UserActivities>($"api/UserActivities/{Id}");
        }

        public async Task<UserActivities> Update(UserActivities person)
        {
            return await httpClient.PutJsonAsync<UserActivities>($"api/UserActivities", person);
        }

        public async Task<List<UserActivities>> GetActivity(int UserID)
        {
            return await httpClient.GetJsonAsync<List<UserActivities>>($"api/UserActivities/List/{UserID}");
        }



        public async Task Delete(int Id)
        {
            await httpClient.DeleteAsync($"api/UserActivities/{Id}");
        }
    }
}

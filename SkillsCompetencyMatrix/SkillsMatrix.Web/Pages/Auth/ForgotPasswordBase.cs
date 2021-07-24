using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;

using NETCore.MailKit.Core;
using MailKit.Net.Smtp;
using System.Net.Mail;
using System.Net;

namespace SkillsMatrix.Web.Pages.Auth
{
    public class ForgotPasswordBase : ComponentBase
    {
        [Inject] AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] IHostEnvironmentAuthenticationStateProvider HostAuthentication { get; set; }

        [Inject]
        protected UserManager<IdentityUser<int>> _userManager { get; set; }
        [Inject]
        protected SignInManager<IdentityUser<int>> _signInManager { get; set; }
        [Inject]
        protected ILogger<RegisterBase> _logger { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        [Inject]
        public IEmailService _emailService { get; set; }

        //protected InputModel Input = new InputModel();

        public class UserLoginDetails
        {
            public string Email { set; get; }
            public string Password { set; get; }
        }
        protected UserLoginDetails userLoginDetails = new UserLoginDetails();

        public bool EmailSent = false;

        protected override Task OnInitializedAsync()
        {
            _logger.LogInformation("OnInitializedAsync started...");

            return base.OnInitializedAsync();

        }


        public async void HandleForgotPassword()
        {
            _logger.LogInformation("HandleForgotPassword started...");
            var user = await _userManager.FindByNameAsync(userLoginDetails.Email);

            if (user!=null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                token = System.Web.HttpUtility.UrlEncode(token);
                var confirmationLink = _navigationManager.BaseUri + "forgotPasswordChange" + "?userId=" + user.Id + "&" + "token=" + token;
                MailMessage msg = new MailMessage();
                //Add your email address to the recipients
                msg.To.Add(user.Email);
                //Configure the address we are sending the mail from
                MailAddress address = new MailAddress("khai.mageba@gmail.com");
                msg.From = address;
                msg.Subject = "Email Verification";
                msg.Body = $"<a href=\"{confirmationLink}\">Verify Email</a>";

                //Configure an SmtpClient to send the mail.            
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.Host = "localhost";
                client.Port = 25;

                //Setup credentials to login to our sender email address ("UserName", "Password")
                //NetworkCredential credentials = new NetworkCredential("khai.mageba@gmail.com", "kasaraji");
                client.UseDefaultCredentials = false;
                //client.Credentials = credentials;

                //Send the msg
                client.Send(msg);

                _navigationManager.NavigateTo("/forgotPasswordMessageSent");
            }
            else
            {
                _logger.LogWarning($"Username {userLoginDetails.Email} is incorrect.");
            }
        }
    }
}

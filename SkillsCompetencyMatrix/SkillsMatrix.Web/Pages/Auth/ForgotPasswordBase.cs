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
using Microsoft.AspNetCore.Components.Forms;
using Blazored.Toast.Services;
using MimeKit;
using MailKit.Security;
using System.Security.Authentication;
using MailKit;
using System.Threading;

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
        [Inject]
        public IToastService toastService { get; set; }

        //protected InputModel Input = new InputModel();

        public class UserLoginDetails
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { set; get; }
            public string Password { set; get; }
        }
        protected UserLoginDetails userLoginDetails = new UserLoginDetails();

        protected EditContext editContext;

        public bool EmailSent = false;

        protected override Task OnInitializedAsync()
        {
            _logger.LogInformation("OnInitializedAsync started...");
            editContext = new EditContext(userLoginDetails);
            return base.OnInitializedAsync();

        }


        public async void HandleForgotPassword()
        {
            _logger.LogInformation("HandleForgotPassword started...");
            var user = await _userManager.FindByNameAsync(userLoginDetails.Email);

            if (user != null)
            {

                try
                {






                    //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    ////token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    //token = System.Web.HttpUtility.UrlEncode(token);
                    //var confirmationLink = _navigationManager.BaseUri + "forgotPasswordChange" + "?userId=" + user.Id + "&" + "token=" + token;
                    //MailMessage msg = new MailMessage();
                    ////Add your email address to the recipients
                    //msg.To.Add(user.Email);
                    ////Configure the address we are sending the mail from
                    //MailAddress address = new MailAddress("khai.mageba@gmail.com");
                    //msg.From = address;
                    //msg.Subject = "Change Password";
                    //msg.Body = $"<a href=\"{confirmationLink}\">Verify Email</a>";

                    ////Configure an SmtpClient to send the mail.            
                    //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.Host = "smtp.gmail.com";
                    //client.Port = 587;
                    //client.UseDefaultCredentials = false;
                    //client.EnableSsl = true;
                    //NetworkCredential credentials = new NetworkCredential("khai.mageba@gmail.com", "Khai.mageba10");
                    //client.Credentials = credentials;

                    ////client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    //client.Send(msg);
                    //_navigationManager.NavigateTo("/forgotPasswordMessageSent");




                    //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    ////token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    //token = System.Web.HttpUtility.UrlEncode(token);
                    //var confirmationLink = _navigationManager.BaseUri + "forgotPasswordChange" + "?userId=" + user.Id + "&" + "token=" + token;
                    //MailMessage msg = new MailMessage();
                    ////Add your email address to the recipients
                    //msg.To.Add(user.Email);
                    ////Configure the address we are sending the mail from
                    //MailAddress address = new MailAddress("noreply@solugrowth.com");
                    //msg.From = address;
                    //msg.Subject = "Email Verification";
                    //msg.Body = $"<a href=\"{confirmationLink}\">Verify Email</a>";

                    ////Configure an SmtpClient to send the mail.            
                    //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.Host = "172.16.201.101";
                    //client.Port = 25;
                    //client.EnableSsl = false;
                    //client.UseDefaultCredentials = false;
                    //NetworkCredential credentials = new NetworkCredential("dtservices1\\Indicium-Dev", "dtssAdmin123!!!");
                    //client.Credentials = credentials;

                    ////client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    //client.Send(msg);
                    //_navigationManager.NavigateTo("/forgotPasswordMessageSent");







                    //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //token = System.Web.HttpUtility.UrlEncode(token);
                    //var confirmationLink = _navigationManager.BaseUri + "forgotPasswordChange" + "?userId=" + user.Id + "&" + "token=" + token;

                    //int port = 25;
                    //string host = "172.16.201.101";
                    //string username = "dtservices1\\Indicium-Dev";
                    //string password = "dtssAdmin123!!!";
                    //string mailFrom = "noreply@solugrowth.com";
                    //string mailTo = user.Email;
                    //string mailTitle = "Password change";
                    //string mailMessage = $"<a href=\"{confirmationLink}\">Verify Email</a>";
                    //var message = new MimeMessage();
                    //message.From.Add(new MailboxAddress(mailFrom));
                    //message.To.Add(new MailboxAddress(mailTo));
                    //message.Subject = mailTitle;
                    //message.Body = new TextPart("plain") { Text = mailMessage };

                    //using (var client = new MailKit.Net.Smtp.SmtpClient(new ProtocolLogger("smtp.log")))
                    //{
                    //    //client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    //    //client.CheckCertificateRevocation = false;
                    //    //client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Ssl2 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
                    //    //client.Connect(host: host, port: port, options: SecureSocketOptions.SslOnConnect);

                    //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    //    client.CheckCertificateRevocation = false;
                    //    client.Connect(host, port, false);
                    //    client.Capabilities &= ~SmtpCapabilities.Pipelining;
                    //    //client.Authenticate(username, password);
                    //    client.Timeout = 50000000;
                    //    client.Send(message);
                    //    client.Disconnect(true);
                    //    _navigationManager.NavigateTo("/forgotPasswordMessageSent");
                    //}




                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    token = System.Web.HttpUtility.UrlEncode(token);
                    var confirmationLink = _navigationManager.BaseUri + "forgotPasswordChange" + "?userId=" + user.Id + "&" + "token=" + token;

                    int port = 25;
                    string host = "172.16.201.60";
                    //string username = "dtservices1\\Indicium-Dev";
                    //string password = "dtssAdmin123!!!";
                    string mailFrom = "noreply@solugrowth.com";
                    string mailTo = user.Email;
                    string mailTitle = "Password change";
                    string mailMessage = $"<a href=\"{confirmationLink}\">Verify Email</a>";
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(mailFrom));
                    message.To.Add(new MailboxAddress(mailTo));
                    message.Subject = mailTitle;
                    message.Body = new TextPart("plain") { Text = mailMessage };

                    using (var client = new MailKit.Net.Smtp.SmtpClient(new ProtocolLogger("smtp.log")))
                    {
                        //client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        //client.CheckCertificateRevocation = false;
                        //client.SslProtocols = SslProtocols.Ssl3 | SslProtocols.Ssl2 | SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
                        //client.Connect(host: host, port: port, options: SecureSocketOptions.SslOnConnect);

                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.CheckCertificateRevocation = false;
                        client.Connect(host, port, false);
                        client.Capabilities &= ~SmtpCapabilities.Pipelining;
                        //client.Authenticate(username, password);
                        client.Timeout = 50000000;
                        client.Send(message);
                        client.Disconnect(true);
                        _navigationManager.NavigateTo("/forgotPasswordMessageSent");
                    }
                }
                catch (Exception ex)
                {
                    toastService.ShowError($"Message {ex.Message} string {ex.ToString()}");
                    if (ex.InnerException != null)
                    {
                        toastService.ShowError($"inner message {ex.InnerException.Message} string {ex.InnerException.ToString()}");
                    }
                }



            }
            else
            {
                _logger.LogWarning($"Username {userLoginDetails.Email} is incorrect.");
                toastService.ShowError($"Username {userLoginDetails.Email} is incorrect.");
            }
        }
    }
}

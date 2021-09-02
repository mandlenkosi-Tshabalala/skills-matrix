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
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Forms;
using MimeKit;
using MailKit.Security;

namespace SkillsMatrix.Web.Pages.Auth
{
    public class RegisterBase : ComponentBase
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

        
        protected EditContext editContext;
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Password must meet requirements")]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        protected InputModel Input = new InputModel();
        protected override Task OnInitializedAsync()
        {
            editContext = new EditContext(Input);

            return base.OnInitializedAsync();
        }
        protected async void HandleValidSubmit()
        {
            var user = new IdentityUser<int> { 
                UserName = Input.Email, 
                Email = Input.Email, 
                EmailConfirmed = false // email not yet confirmed at register
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                




                try
                {
                    //create userrole
                    _logger.LogInformation("User created a new account with password.");

                    //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //token = System.Web.HttpUtility.UrlEncode(token);
                    //var confirmationLink = _navigationManager.BaseUri + "ConfirmEmail" + "?userId=" + user.Id + "&" + "token=" + token;
                    //MailMessage msg = new MailMessage();
                    ////Add your email address to the recipients
                    //msg.To.Add(user.Email);
                    ////Configure the address we are sending the mail from
                    //MailAddress address = new MailAddress("noreply@solugrowth.com");
                    //msg.From = address;
                    //msg.Subject = "Email Verification";
                    //msg.Body = $"<a href=\"{confirmationLink}\">Verify Email</a>";

                    //Configure an SmtpClient to send the mail.            
                    //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.Host = "smtp.gmail.com";
                    //client.Port = 587;
                    //client.UseDefaultCredentials = true;
                    //NetworkCredential credentials = new NetworkCredential("zibhovazi@gmail.com", "playmaker10");
                    //client.Credentials = credentials;
                    //client.EnableSsl = true;

                    //client.Send(msg);

                    //_navigationManager.NavigateTo("/RegisterConfirmation");




                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    token = System.Web.HttpUtility.UrlEncode(token);
                    var confirmationLink = _navigationManager.BaseUri + "ConfirmEmail" + "?userId=" + user.Id + "&" + "token=" + token;

                    int port = 25;
                    string host = "172.16.201.101";
                    string username = "dtservices1\\Indicium-Dev";
                    string password = "dtssAdmin123!!!";
                    string mailFrom = "noreply@solugrowth.com";
                    string mailTo = user.Email;
                    string mailTitle = "Email Verification";
                    string mailMessage = $"<a href=\"{confirmationLink}\">Verify Email</a>";
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(mailFrom));
                    message.To.Add(new MailboxAddress(mailTo));
                    message.Subject = mailTitle;
                    message.Body = new TextPart("plain") { Text = mailMessage };

                    using (var client = new MailKit.Net.Smtp.SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        client.Connect(host, port, false);
                        //client.Authenticate(username, password);

                        client.Send(message);
                        client.Disconnect(true);
                        _navigationManager.NavigateTo("/RegisterConfirmation");
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



                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                //var confirmationLink = _navigationManager.BaseUri + "Auth/ConfirmEmail" + user.Id + token;


                //await _emailService.SendAsync(user.Email," Email Verification", confirmationLink);
                //Create the msg object to be sent








                //using (var client = new SmtpClient())
                //{
                //    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                //    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //    Console.WriteLine("Connect");
                //    client.Connect("smtp.gmail.com", 587, true); 
                //    client.Authenticate("tfs.gettaxsolutions", "*********");

                //    Console.WriteLine("Send Message");
                //    client.Send(message);
                //    Console.WriteLine("Disconnect");
                //    client.Disconnect(true);
                //}



                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
                //var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
                //await _emailSender.SendEmailAsync(message);


                ////Generate Email confirmation link
                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                //var s= _navigationManager.BaseUri + "/Pages/Auth";
                //var callbackUrl = Url.Page(
                //    "/Account/ConfirmEmail",
                //    pageHandler: null,
                //    values: new { area = "Identity", userId = user.Id, code = code },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");






                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                //var callbackUrl = Url.Page(
                //    "/Account/ConfirmEmail",
                //    pageHandler: null,
                //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                //{
                //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                //}
                //else
                //{
                //    await _signInManager.SignInAsync(user, isPersistent: false);
                //    return LocalRedirect(returnUrl);
                //}

                //var principal = await _signInManager.CreateUserPrincipalAsync(user);

                //var identity = new ClaimsIdentity(
                //    principal.Claims,
                //    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme
                //);

                //principal = new ClaimsPrincipal(identity);

                //_signInManager.Context.User = principal;

                //HostAuthentication.SetAuthenticationState(Task.FromResult(new AuthenticationState(principal)));

                //// now the authState is updated
                //var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

                //_navigationManager.NavigateTo("/");
            }

            else
            {
                toastService.ShowError("There was an error creating the user.");
            }
        }
    }
}

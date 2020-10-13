﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skclusive.Blazor.Dashboard.App.View.Authentication
{
    public class SignInPageBase : ComponentBase
    {
        public string Email { set; get; }

        public string Password { set; get; }

        public void HandleEmailChange(ChangeEventArgs arg)
        {
            Email = arg.Value.ToString();

            StateHasChanged();
        }

        public void HandlePasswordChange(ChangeEventArgs arg)
        {
            Password = arg.Value.ToString();

            StateHasChanged();
        }

        public void HandleSignIn()
        {
            System.Console.WriteLine("HandleSignIn");

            //_ = ScriptHelpers.GoBackAsync();
            //use navigation manager

            System.Console.WriteLine("HandleSignInDone");
        }
    }
}

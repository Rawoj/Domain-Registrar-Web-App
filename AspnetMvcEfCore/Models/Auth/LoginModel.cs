using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainRegistrarWebApp.Models.Auth
{
    public class LoginModel 
    {
        public string S { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(S);
    }
}

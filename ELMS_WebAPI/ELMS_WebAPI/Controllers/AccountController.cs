using ELMS_WebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ELMS_WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(AccountModel accountModelObj)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser() { UserName = accountModelObj.Email, Email = accountModelObj.Email };

            user.Name = accountModelObj.Name;
            user.Address = accountModelObj.Address;
            user.PhoneNumber = accountModelObj.PhoneNumber.ToString();
            user.Interest = accountModelObj.Interest;
            user.Bank_Name = accountModelObj.Bank_Name;
            user.Credit_Card = accountModelObj.Credit_Card;
            user.Subscription = accountModelObj.Subscription;
            user.Admin = accountModelObj.Admin;
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5
            };
            IdentityResult result = manager.Create(user, accountModelObj.Password);
            return result;
        }

        [HttpGet]
        [Route("api/GetUserClaims")]
        
        public AccountModel GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            AccountModel model = new AccountModel()
            {
                UserName = identityClaims.FindFirst("Username").Value,
                Name = identityClaims.FindFirst("Name").Value,
                Address = identityClaims.FindFirst("Address").Value,
                PhoneNumber = Convert.ToInt64(identityClaims.FindFirst("PhoneNumber").Value),
                Interest = identityClaims.FindFirst("Interest").Value,
                Email = identityClaims.FindFirst("Email").Value,

                Bank_Name = identityClaims.FindFirst("Bank_Name").Value,
                Credit_Card = Convert.ToInt64(identityClaims.FindFirst("Credit_Card").Value),
                Subscription = Convert.ToBoolean(identityClaims.FindFirst("Subscription").Value),
                Admin = Convert.ToBoolean(identityClaims.FindFirst("Admin").Value),
                LoggedOn = identityClaims.FindFirst("LoggedOn").Value
            };
            return model;
        }

    }
}

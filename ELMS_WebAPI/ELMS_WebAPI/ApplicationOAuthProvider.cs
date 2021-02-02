using ELMS_WebAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ELMS_WebAPI
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            context.Validated();
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = await manager.FindAsync(context.UserName, context.Password);
            if (user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim("Username", user.Email));
                identity.AddClaim(new Claim("Name", user.Name));
                identity.AddClaim(new Claim("Address", user.Address));
                identity.AddClaim(new Claim("PhoneNumber", user.PhoneNumber));
                identity.AddClaim(new Claim("Interest", user.Interest));
                identity.AddClaim(new Claim("Email", user.Email));
                //identity.AddClaim(new Claim("Password", user.Password));
                identity.AddClaim(new Claim("Bank_Name", user.Bank_Name));
                identity.AddClaim(new Claim("Credit_Card", user.Credit_Card.ToString()));
                identity.AddClaim(new Claim("Subscription", user.Subscription.ToString()));
                identity.AddClaim(new Claim("Admin", user.Admin.ToString()));
                identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));

                context.Validated(identity);
            }
            else
            {
                return;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ELMS_WebAPI.Models
{
    public class AccountModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }
        public string Interest { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Bank_Name { get; set; }
        public Nullable<long> Credit_Card { get; set; }
        public Nullable<bool> Subscription { get; set; }
        public Nullable<bool> Admin { get; set; }
        public string LoggedOn { get; set; }
    }
}
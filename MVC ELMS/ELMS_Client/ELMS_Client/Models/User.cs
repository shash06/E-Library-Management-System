using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELMS_Client.Models
{
    public class User
    {
        [Display(Name = "User ID")]
        public int User_id { get; set; }

        [Required]
        [RegularExpression("^[A-Z][A-Za-z ]+$", ErrorMessage = "Name must be in Alphabets Only and should start with an Uppercase character ")]
        [MaxLength(20,ErrorMessage ="Name cannot be more than 20 charcaters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Address cannot be more than 50 charcaters")]
        public string Address { get; set; }

        [Required]
        [RegularExpression("[6-9][0-9]{9}", ErrorMessage = "Phone Number is Invalid.")]
        [Display(Name = "Phone Number")]
        public Nullable<long> Phone_Number { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Interest cannot be more than 50 charcaters")]
        public string Interest { get; set; }

        [Required]
        //[EmailAddress(ErrorMessage ="Email Address is not in correct format")]
        [RegularExpression("^[A-Za-z0-9.]+[@][A-Za-z]+[.][A-Za-z]+$", ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID")]
        [MaxLength(30, ErrorMessage = "Email ID cannot be more than 30 charcaters")]
        public string Email_id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Password cannot be less than 5 characters")]
        [MaxLength(10, ErrorMessage = "Password cannot be more than 10 characters")]
        //[Range(5,10,ErrorMessage ="Password should be between 5 to 10 characters")]
        public string Password { get; set; }
 
        [Display(Name = "Bank")]
        public string Bank_Name { get; set; }
 
        [RegularExpression("[1-9][0-9]{15}", ErrorMessage = "Credit Card Number is Invalid.")]
        [Display(Name = "Credit Card")]
        public Nullable<long> Credit_Card { get; set; }

        public bool Subscription { get; set; }

        public bool Admin { get; set; }

    }
}
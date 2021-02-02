using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELMS_Client.Models
{
    public class Document
    {
            [Display(Name = "Document ID")]
            public int Doc_id { get; set; }

            [Required]
            [RegularExpression("^[A-Za-z0-9.!@#$%^&*() ]+$", ErrorMessage = "Invalid Title.")]
            [MaxLength(15,ErrorMessage ="Title cannot be more than 15 characters. ")]
            public string Title { get; set; }

            [Required]
            [MaxLength(200,ErrorMessage = "Description cannot be more than 200 characters.")]
            public string Description { get; set; }

            [Required]
            [Display(Name = "Discipline")]
            public Nullable<int> Discipline_id { get; set; }
        
            [Required]
            [RegularExpression("^[A-Z][A-Za-z ]*", ErrorMessage = "Author can be alphabets only.")]
            public string Author { get; set; }

            [Required]
            public Nullable<System.DateTime> Date { get; set; }
            
            public Nullable<decimal> Price { get; set; }
            [Display(Name = "Document Type")]
            public Nullable<int> Doc_TypeId { get; set; }

        
    }
}
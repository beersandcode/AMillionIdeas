﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AMillionIdeas.Models
{
    [MetadataType(typeof(InfoUserMetada))]
    public partial class InfoUsers
    {
        class InfoUserMetada
        {
            [Display(Name = "User Name")]
            [Required(ErrorMessage = "UserName is required")]
            public string UserName { get; set; }

            [Display(Name = "User Password")]
            [MinLength(4)]
            [Required(ErrorMessage = "UserPass is required")]
            public string UserPass { get; set; }

            [Display(Name = "User Email")]
            [Required(ErrorMessage = "Email is required")]
            public string Email { get; set; }

        }
    }
}
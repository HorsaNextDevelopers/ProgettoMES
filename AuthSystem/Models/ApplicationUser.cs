using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Xamarin.Essentials;

namespace AuthSystem.Areas.Identity.Data
{
    
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [DisplayName("Cognome")]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [PersonalData]
        [DisplayName("Nome")]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

    }

}

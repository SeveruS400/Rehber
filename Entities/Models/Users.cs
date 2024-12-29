﻿
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Users : IdentityUser
    {
        public int Id { get; set; }
        public int SessionTimeoutInHours { get; set; }  // Örneğin, oturum süresi
        public string AddedByUserName { get; set; }


    }
}
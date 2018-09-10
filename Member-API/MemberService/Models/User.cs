using System.ComponentModel.DataAnnotations;
﻿using MemberService.Models.Exceptions;
using System;
using MemberService.DataTypes.Enums;

namespace MemberService.Models
{
    public class User
    {
        [Required]
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Github { get; set; }
        public Level Level { get; set; }
    }
}
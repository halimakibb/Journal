using System;
using System.ComponentModel.DataAnnotations;

namespace Journal.Entities
{
    public class User
    {
        [Key]
        public Int64 UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
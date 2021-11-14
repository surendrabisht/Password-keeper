using System;
using System.ComponentModel.DataAnnotations;

namespace PasswordKeeper.Web
{
    public class CredentialsDTO
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Pwd { get; set; }
    }
}

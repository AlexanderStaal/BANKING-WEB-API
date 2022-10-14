using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace BankingWebAPI.Models
{
    [Table("Users")]
    public class User
    {
        [Required]
        [DataMember(Name = "key")]
        [Key]
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userRole { get; set; }
    }

    [Table("LoginStatus")]
    public class LoginStatus
    {
        [Required]
        [DataMember(Name = "key")]
        [Key]
        public string loginStatus { get; set; }
    }
}
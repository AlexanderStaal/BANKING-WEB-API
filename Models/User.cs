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
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string role { get; set; }     
        public string email { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace BankingWebAPI.Context
{
    [Table("Account")]
    public class Accounts
    {
        [Required]
        [DataMember(Name = "key")]
        [Key]
        public int accountNumber { get; set; }
        public string accountName { get; set; }
        public double balance { get; set; }
    }

    [Table("CreateAccount")]
    public class CreateAccount
    {
        [Required]
        [DataMember(Name = "key")]
        [Key]
        public string ReturnValue { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace BankingWebAPI.Models
{
    [Table("Transactions")]
    public class TransactionsHistory
    {
        [Required]
        [DataMember(Name = "key")]
        [Key]
        public int transactionId { get; set; }
        public int fromAccountNumber { get; set; }
        public int toAccountNumber { get; set; }
        public DateTime transactionTime { get; set; }
        public double amountDebit { get; set; }
        public double fromAccountBalance { get; set; }
        public double toAccountBalance { get; set; }
    }

    [Table("TransferFunds")]
    public class TransferFunds
    {
        [Required]
        [DataMember(Name = "key")]
        [Key]
        public int fromAccountNumber { get; set; }
        public int toAccountNumber { get; set; }
        public double amount { get; set; }
    }

    [Table("TransferFundsStatus")]
    public class TransferFundsStatus
    {
        [Required]
        [DataMember(Name = "key")]
        [Key]
        public string transferFundsStatus { get; set; }
    }

    [Table("TransferSource")]
    public class TransferSource
    {
        [Required]
        [DataMember(Name = "key")]
        [Key]
        public int value { get; set; }
        public string label { get; set; }
    }
}


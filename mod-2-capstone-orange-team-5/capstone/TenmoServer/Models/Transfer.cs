using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }
        public int TransferTypeId  { get; set; }
        // get id
        // list all id's
        public int TransferStatusId { get; set; }
        // get id
        //list all id's
        public int accountFrom { get; set; }
        //get accountFrom
        public int accountTo { get; set; }
        //get accountIn
        public decimal amount { get; set; }
        //get amount
    }
}

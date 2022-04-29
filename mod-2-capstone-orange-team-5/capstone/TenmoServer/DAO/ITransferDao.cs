using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    interface ITransferDao
    {
        Transfer Create(Transfer transfer); 
        //Transfer GetTransferId(int id);
        IList<Transfer> FindByTransferId(int id);

       // Transfer TransferStatusId(int id);

        IList<Transfer> FindByTransferStatusId(int id);

        Transfer GetTransfer(int id);

        IList<Transfer> List();
        Transfer Update(int id, Transfer updated);








        //public int transferTypeId { get; set; }
        //// get id
        //// list all id's
        //public int transferStatusId { get; set; }
        //// get id
        ////list all id's
        //public int accountFrom { get; set; }
        ////get accountFrom
        //public int accountIn { get; set; }
        ////get accountIn
        //public decimal amount { get; set; }
        ////get amount
    }
}

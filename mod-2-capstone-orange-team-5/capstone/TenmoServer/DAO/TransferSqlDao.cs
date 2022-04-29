using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDao : ITransferDao
    {

        private readonly string connectionString;

        public TransferSqlDao(string connString)
        {
            connectionString = connString;
        }

        public Transfer Create(Transfer transfer)
        {
            //return the id back 
            int newTransferId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlText = "INSERT INTO transfer (account_from, account_to, amount) " +
                                                "OUTPUT INSERTED.transfer_id " +
                                                "VALUES (@account_from, @account_to, @balance);";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                cmd.Parameters.AddWithValue("@account_from", transfer.accountFrom);
                cmd.Parameters.AddWithValue("@account_to", transfer.accountTo);
                cmd.Parameters.AddWithValue("@amount", transfer.amount);

                newTransferId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return GetTransfer(newTransferId);

        }

        public IList<Transfer> FindByTransferId(int id)
        {
            IList<Transfer> transferList = new List<Transfer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //*************************************************************
                //Might have to change the Sql quarry depending on results
                string sqlTest = "SELECT * FROM transfer WHERE transfer_type_id = @transfer_type_id ";
                SqlCommand cmd = new SqlCommand(sqlTest, conn);

                cmd.Parameters.AddWithValue("@tansfer_type_id", id);
               


                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Transfer transfer = CreateTransferFromReader(reader);

                    transferList.Add(transfer);
                }


            }

            return transferList;
        }

        public IList<Transfer> FindByTransferStatusId(int id)
        {
            IList<Transfer> transferList = new List<Transfer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sqlTest = "SELECT * FROM transfer WHERE transfer_status_id = @transfer_status_id ";
                SqlCommand cmd = new SqlCommand(sqlTest, conn);

                cmd.Parameters.AddWithValue("@tansfer_status_id", id);



                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Transfer transfer = CreateTransferFromReader(reader);

                    transferList.Add(transfer);
                }


            }

            return transferList;
        }

        public Transfer GetTransfer(int id)
        {
            Transfer transfer = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM transfer WHERE transfer_id = @tansfer_id", conn);
                cmd.Parameters.AddWithValue("@transfer_id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    transfer = CreateTransferFromReader(reader);
                }
              
            }

            return transfer;


        }

        public IList<Transfer> List()
        {
            IList<Transfer> transfersList = new List<Transfer>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM transfer", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Transfer transfer = CreateTransferFromReader(reader);
                    transfersList.Add(transfer);
                }
               

            }
            return transfersList;
        }

        public Transfer Update(int id, Transfer updated)
        {
            throw new NotImplementedException();
        }

        private Transfer CreateTransferFromReader(SqlDataReader reader)
        {

            Transfer transfer = new Transfer();
            transfer.TransferId = Convert.ToInt32(reader["transfer_id"]);
            transfer.TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]);
            transfer.TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]);
            transfer.accountFrom = Convert.ToInt32(reader["account_from"]);
            transfer.accountTo = Convert.ToInt32(reader["account_to"]);
            transfer.amount = Convert.ToDecimal(reader["amount"]);

            return transfer;


        }
    }
}

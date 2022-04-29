using TenmoServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TenmoServer.DAO
{
    public class AccountSqlDao : IAccountDao
    {
        private readonly string connectionString;

        public AccountSqlDao(string connString)
        {
            connectionString = connString;
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                string sqlText = "DELETE FROM account WHERE account_id = @account_id";
                conn.Open();

                // must delete records in park_state first
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                cmd.Parameters.AddWithValue("@account_id",id);

                cmd.ExecuteNonQuery();

                
            }
        }

        public Account Create(Account account)
        {
            int newProjectId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sqlText = "INSERT INTO account (account_id, user_id, balance) " +
                                                 
                                                "VALUES (@account_id, @user_id, @balance);";
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlText, conn);
                cmd.Parameters.AddWithValue("@account_id", account.accountId);
                cmd.Parameters.AddWithValue("@user_id", account.userId);
                cmd.Parameters.AddWithValue("@balance", account.balance);
               
               // newProjectId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            //return GetProject(newProjectId);
            return account;
        }

        public Account GetAccount(int id)
        {
            Account account = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT account_id, user_id, balance FROM account WHERE account_id = @account_id;", conn);
                cmd.Parameters.AddWithValue("@account_id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    account = CreateAccountFromReader(reader);
                }
            }
            return account;

        }

        //public void Update(int id, Account account)
        //{
        //    // Department department = new Department();

        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        SqlCommand cmd = new SqlCommand("UPDATE account SET user_id = @user_id AND balance = @balance WHERE account_id = @account_id", conn);
        //        cmd.Parameters.AddWithValue("@account_id", id);
        //        cmd.Parameters.AddWithValue("@user_id", account.userId);
        //        cmd.Parameters.AddWithValue("@balance", account.balance);
        //        cmd.ExecuteNonQuery();
        //    }

        //}

        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM account;", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Account account = CreateAccountFromReader(reader);
                    accounts.Add(account);
                }
            }

            return accounts;
        }

        

      

        private Account CreateAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account();
            account.accountId = Convert.ToInt32(reader["account_id"]);
            account.userId = Convert.ToInt32(reader["user_id"]);
            account.balance = Convert.ToDecimal(reader["balance"]);


            return account;
        }

        public Account Update(int id, Account account)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE account SET user_id = @user_id, balance = @balance WHERE account_id = @account_id", conn);
                cmd.Parameters.AddWithValue("@account_id", id);
                cmd.Parameters.AddWithValue("@user_id", account.userId);
                cmd.Parameters.AddWithValue("@balance", account.balance);
                cmd.ExecuteNonQuery();
            }

            return null;
        }
    }
}


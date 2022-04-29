using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Controllers;
using TenmoServer.Models;


namespace TenmoServer.Controllers
{
    // api/
    [Route("[Controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDao accountDao;


        public AccountController(IAccountDao accountDao)
        {
            this.accountDao = accountDao;
        }

        [HttpGet()]
        public List<Account> ListAccounts()
        {
            return accountDao.GetAllAccounts();
        }


        ////private readonly IAuctionDao auctionDao;

        ////public AuctionsController(IAuctionDao auctionDao)
        ////{
        ////    this.auctionDao = auctionDao;


        [HttpPost]
        public ActionResult<Account> Create(Account account)
        {

            Account created = accountDao.Create(account);
            return Created($"/auctions/{created.accountId}", created);
        }
        //[Authorize(Roles = "creator, admin")]
        
        [HttpGet("{id}")]
        public ActionResult<Account> GetAccountByUser(int id)
        {
            Account account = accountDao.GetAccount(id);

            return account;
        }





        [HttpPut("{id}")]
        public ActionResult<Account> Update(int id, Account updatedAccount)
        {
            Account existingAccount = accountDao.GetAccount(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            Account result = accountDao.Update(id, updatedAccount);
            return Ok(result);
        }





        ////[HttpDelete("{id}")]
        ////public ActionResult Delete(int id)
        ////{
        ////    Auction auction = auctionDao.Get(id);
        ////    if (auction == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    bool result = auctionDao.Delete(id);
        ////    if (result)
        ////    {
        ////        return NoContent();
        ////    }
        ////    else
        ////    {
        ////        return BadRequest();
        ////    }
        ////}

    }
}

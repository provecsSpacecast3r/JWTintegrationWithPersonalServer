using FirstProject.ATM;
using FirstProject.InputReq;
using FirstProject.Persons;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CRUDtest.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class ATM_Controller : Controller
    {
        
        private IDataProtector _protector;
        public ATM_Controller(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector(GetType().FullName);
        }

        /// <response code="200">When the pin and the amount are correct</response>
        /// <response code="412">When you enter invalid input</response>

        [HttpPost("PostATM")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status412PreconditionFailed)]
        public IActionResult Post(BoundedReq boundedReq)
        {
            var foo = new User("12345", _protector);
            var ATM = new CashMachine();
            var status = foo.Withdraw(ATM, boundedReq.Taker.Amount).StatusCode;

            if (status == 200) { return new ObjectResult(foo.Withdraw(ATM, boundedReq.Taker.Amount)); }
            
            else { return new ObjectResult(foo.Withdraw(ATM, boundedReq.Taker.Amount)) { StatusCode = (int) HttpStatusCode.PreconditionFailed}; }
        }
        
    }
}
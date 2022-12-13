using CRUDtest.Person;
using FirstProject.InputReq;

namespace CRUDtest.Controllers
{
    public class BoundedReq
    {
        public InputReq Taker { get; set; }
        public PersonLogin UserLogging { get; set; }
    }
}

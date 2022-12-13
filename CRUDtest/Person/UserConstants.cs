using CRUDtest.Controllers;
using FirstProject.InputReq;
using FirstProject.Persons;
using Microsoft.AspNetCore.DataProtection;

namespace CRUDtest.Person
{
    public class UserConstants
    {
        public static List<User> notCapableOfDoingDatabases(IDataProtector protector, BoundedReq boundedReq)
        {

            List<User> logs = new List<User>()
            {
                new User(protector.Protect(boundedReq.Taker.Pin), protector){Username = "nicotra", Role = "admin", Password = "NarutoCoerente"}
            };

            return logs;
        }
    }
}

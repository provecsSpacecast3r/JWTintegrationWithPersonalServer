using System.ComponentModel.DataAnnotations;

namespace CRUDtest.Person
{
    public class PersonLogin
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
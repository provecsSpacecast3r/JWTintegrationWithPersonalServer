using System.ComponentModel.DataAnnotations;

namespace FirstProject.InputReq
{
    public class InputReq
    {
        ///<summary>
        /// Enter a pin made of at least 5 numbers and insert the amount of money you want to withdraw.
        /// </summary>
        /// 
        [Required]
        public string Pin { set; get; }
        public string Amount { set; get; }
    }
}

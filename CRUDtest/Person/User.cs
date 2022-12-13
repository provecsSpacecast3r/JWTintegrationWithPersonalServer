using FirstProject.ATM;
using FirstProject.Card;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Persons
{
  public class User
  {
    private string _pin;
    private ATMCard _card;
    public string Role { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    private IDataProtector _protector;

        public User(string pin, IDataProtector protector)
        {
            this._protector = protector;
            this._card = new ATMCard(this._protector);
            this._pin = wichPin(pin);
        }

        private string wichPin(string pin)
        { 
            var tempPin = _protector.Unprotect(pin);

            try
                {

                    if (tempPin.Length != 5)
                    {
                        return "wrong";
                    }
                }
                catch (Exception err)
                {
                    if (err != null)
                    {
                        return "wrong";
                    }
                }

                return pin;
        }


        public string getPin()
    {
      return this._pin;
    }


    public ObjectResult Withdraw(CashMachine machine, string amount)
    {
            return machine.Withdraw(this, _card, amount);
    }
  }
}
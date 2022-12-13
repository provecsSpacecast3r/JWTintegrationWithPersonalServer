using System.Net;
using FirstProject.Card;
using FirstProject.Persons;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.ATM
{
  public class CashMachine : EssentialFeatures
  {

    public ObjectResult Logging(User foo, ATMCard input)
    {
        if (input.comparePin(foo.getPin()))
        {
          return new ObjectResult("ok") { StatusCode = (int)HttpStatusCode.Accepted};
        }else
        {
          return new ObjectResult("<--------- Invalid PIN Given (non fare il ladro a casa dei ladri) --------->") { StatusCode = (int)HttpStatusCode.PreconditionFailed};
        }
    }

    public ObjectResult Withdraw(User foo, ATMCard input, string amount)
    {
      bool controller = true;
      UInt32 workingVar = 0;

      do
      {
        try
        {
            workingVar = Convert.ToUInt32(amount);

            if (workingVar < 0)
            {
                        controller = false;
                        return new ObjectResult("mistyped input, try again: ") { StatusCode = (int)HttpStatusCode.PreconditionFailed };
            }else {controller = true;}
        }catch(Exception err)
          {
            if (err != null || workingVar < 0)
            {
                        controller = false;
                        return new ObjectResult("mistyped input, try again: ") { StatusCode = (int)HttpStatusCode.PreconditionFailed }; ;
            }
          }
      }while (!controller);

      var convertedBalance = Convert.ToInt64(input.getBalance());

            if (Logging(foo, input).StatusCode == 202)
            {
                if (convertedBalance < workingVar)
                {
                    return new ObjectResult("\"<--------- Not enough money --------->\";\r\n                }") { StatusCode = (int)HttpStatusCode.PreconditionFailed };
                }
                else if (workingVar > 1000)
                {
                    return new ObjectResult("\"<--------- Withdraw limit is 1000$ --------->\"") { StatusCode = (int)HttpStatusCode.PreconditionFailed };
                }
                else
                {
                    var onActivity = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                    var updatedBalance = convertedBalance - workingVar;

                    input.updateBalance(updatedBalance.ToString());
                    input.updateTrasactionRegister($"Activity on {onActivity}: withdraw of {amount}");

                    return new ObjectResult($"\nyour new balance is {input.getBalance()}") { StatusCode = (int)HttpStatusCode.OK };
                }
            }else { return new ObjectResult(Logging(foo,input)) { StatusCode = (int)HttpStatusCode.PreconditionFailed}; }
    }


    public void LastTransactions(ATMCard input)
    {
      var fi = input.GetLastTransactions();
      FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Read , FileShare.Read); 

      StreamReader sr = new StreamReader(fs);
      
      string fileContent = sr.ReadToEnd();
      
      sr.Close();
      fs.Close();

      Console.WriteLine(fileContent);
    }
  }
}

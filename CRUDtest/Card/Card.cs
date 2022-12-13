using Microsoft.AspNetCore.DataProtection;

namespace FirstProject.Card
{
  public class ATMCard
  {
    private string _pin;
    private string _cardNumber;
    private string _balance;
    private FileInfo _lastTransactions;
    private IDataProtector _protector;

    public ATMCard(IDataProtector protector)
    {
      this._pin = "12345"; //sostituire con createPin() dopo la fase di testing
      this._balance = randomBalance();
      this._cardNumber = createCardCode();
      this._lastTransactions = new FileInfo($@"Card/registerOf{this._cardNumber}.txt");
      this._protector = protector;
    }
    

    private string createPin()
    {
      var generatedPin = "";
      Random rnd = new Random();

      for (int i = 0; i < 5; i++)
      {
        int num = rnd.Next(0,9);
        generatedPin += num.ToString();
      }
      Console.WriteLine(generatedPin);
      return generatedPin;
    }

    private string createCardCode()
    {
      var generatedCode = "";
      Random rnd = new Random();

      for (int i = 0; i < 16; i++)
      {
        int num = rnd.Next(0,9);
        generatedCode += num.ToString();
      }
      return generatedCode;
    }

    private string randomBalance()
    {
      var generatedBalance = "";
      Random rnd = new Random();

      for (int i = 0; i < rnd.Next(1,9); i++)
      {
        int num = rnd.Next(0,9);
        generatedBalance += num.ToString();
      }
      return generatedBalance;
    }

    public bool comparePin(string pin)
    {
      if (this._pin == _protector.Unprotect(pin))
      {
        return true;
      }

      return false;
      
    }

    public string getCardNumber()
    {
      return this._cardNumber;
    }

    public string getBalance()
    {
      return _balance;
    }

    public void updateBalance(string balance)
    {
      this._balance = balance;
    }

    public void updateTrasactionRegister(string update)
    {

      FileStream fs = _lastTransactions.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read ); 
      StreamWriter sw = new StreamWriter(fs);
      
      sw.WriteLine (update + "\n");
      
      sw.Close();
      fs.Close();
    }

    public FileInfo GetLastTransactions()
    {
      return this._lastTransactions;
    }

    
  }
}
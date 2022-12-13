using FirstProject.Card;
using FirstProject.Persons;
using Microsoft.AspNetCore.Mvc;

interface EssentialFeatures
{
  ObjectResult Logging(User foo, ATMCard input);
  ObjectResult Withdraw(User foo, ATMCard input, string amount);
  void LastTransactions(ATMCard input);
}
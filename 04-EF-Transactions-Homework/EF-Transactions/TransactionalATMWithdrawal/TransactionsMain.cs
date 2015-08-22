using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TransactionalATMWithdrawal
{
    class TransactionsMain
    {
        static void Main()
        {
            // valid
            WithdrawAmount("1111334455", "2222", 150m);
            WithdrawAmount("1122334455", "1111", 200m);


            // invalid - pin
            // WithdrawAmount("1122334455", "1110", 100m);

            // invalid - cardNumber
            // WithdrawAmount("1122334456", "1111", 100m);

            // invalid - insufficient availabilit
            // WithdrawAmount("3322334455", "4444", 2000m);
        }

        static void WithdrawAmount(string cardNumber, string cardPIN, decimal amount)
        {
            var context = new ATMEntities();
            using (var dbTransaction = context.Database.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                try
                {
                    var account = GetValidAccount(context, cardNumber, cardPIN);
                    CheckBallance(account, amount);
                    account.CardCash -= amount;

                    // History Table
                    context.TransactionHistories.Add(new TransactionHistory
                                                     {
                                                         CardNumber = account.CardNumber,
                                                         TransactionDate = DateTime.Now,
                                                         Amount = amount
                                                     });

                    context.SaveChanges();
                    dbTransaction.Commit();

                    Console.WriteLine("Transaction complete.");

                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    Console.WriteLine(ex.Message);
                }

            }
        }

        private static void CheckBallance(CardAccount account, decimal amount)
        {
            if (account.CardCash <= amount)
            {
                throw new InvalidOperationException("insufficient availability");
            }
        }

        private static CardAccount GetValidAccount(ATMEntities context, string cardNumber, string cardPIN)
        {
            try
            {
                var account = context.CardAccounts.Single(a => a.CardNumber == cardNumber && a.CardPIN == cardPIN);
                return account;
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid Card Number or PIN number!");
            }
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kata.bank
{
    public interface IStatementPrinter{
        string Print(List<Transaction> transactions);
    }
    public class StatementPrinter:IStatementPrinter
    {
        public string Print(List<Transaction> transactions){
            
            StringBuilder builder = new StringBuilder();

            var balance = 0;
            foreach(var transaction in transactions.OrderByDescending(x=>x.Date)){
                balance += transaction.Value;
                var transactionAsString = $"{transaction.Date.ToShortDateString()},{transaction.Value},{balance};";
                builder.Append(transactionAsString);
            }
            
            return builder.ToString();
        }

    }
}

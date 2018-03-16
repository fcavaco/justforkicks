using System;
using System.Collections.Generic;

namespace kata.bank
{

    public class Account{

        private List<Transaction> _transactions;

        private readonly IStatementPrinter _statementPrinter;
        public Account(IStatementPrinter statementPrinter)
        {
            _statementPrinter = statementPrinter;
            _transactions =  new List<Transaction>();
        }

        
        public void AddTransaction(DateTime date, int value)
        {
            var transaction = new Transaction(date,value);
            _transactions.Add(transaction);
            _statementPrinter.Print(_transactions);
        }


    }
}

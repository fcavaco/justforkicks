using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;

namespace kata.bank.tests
{
    public class AccountTests
    {
        private Mock<IStatementPrinter> _statementPrinterMock;
        private Account _account;
        [SetUp]
        public void Setup()
        {

            _statementPrinterMock = new Mock<IStatementPrinter>();
            _account = new Account(_statementPrinterMock.Object);
        }

        [Test]
        [TestCase(1000, "10/01/2012")]
        [TestCase(-1000, "10/01/2012")]
        public void Account_can_add_transaction(int value, string dateString)
        { 

            var date = DateTime.Parse(dateString);
            
            _account.AddTransaction(date, value);
            _statementPrinterMock.Verify(x=>x.Print(It.IsAny<List<Transaction>>()), Times.AtLeastOnce);
        }

        [Test]
        [TestCase("14/01/2012",100,"14/01/2012,100,100;")]
        [TestCase("14/01/2012",-100,"14/01/2012,-100,-100;")]
        public void StatementPrinter_can_print_transaction(string dateString, int value, string expected){
            
            var date = DateTime.Parse(dateString);
            var transaction = new Transaction(date, value );
            var transactions = new List<Transaction>();
            transactions.Add(transaction);

            var statementPrinter = new StatementPrinter();
            var result = statementPrinter.Print(transactions);
            Assert.AreEqual(expected, result);    

        }

        [Test]
        public void StatementPrinter_can_create_statement_as_defined_Kata(){
            
            
            var transactions = new List<Transaction>(){
                new Transaction(DateTime.Parse("14/01/2012"), -500 ),
                new Transaction(DateTime.Parse("13/01/2012"), 2000 ),
                new Transaction(DateTime.Parse("10/01/2012"), 1000 )
            };

            var printer = new StatementPrinter();
            var result = printer.Print(transactions);
            var expected = $"14/01/2012,-500,2500;13/01/2012,2000,3000;10/01/2012,1000,1000;";

            Assert.AreEqual(expected, result);
        }
    }
}
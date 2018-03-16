using System;

namespace kata.bank
{
    public class Transaction{
        private readonly DateTime _date;
        private readonly int _value;
        public Transaction(DateTime date,int value ){
            _date = date;
            _value = value;
        }  

        public int Value => _value;
        public DateTime Date => _date;
    }
}

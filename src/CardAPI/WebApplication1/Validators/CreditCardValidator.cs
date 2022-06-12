using Card.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CardAPI.Validators
{
    public static class CreditCardValidator
    {

        public static bool IsCardNumberValid(this CreditCard creditCard)
        {
            if (!Regex.IsMatch(creditCard.CardNumber, @"^[0-9]+$"))
                return false;

            return LuhnCheck(creditCard.CardNumber);
        }

        private static bool LuhnCheck(string cardNumber)
        {
            int[] digits = new int[cardNumber.Length];
            int sum = 0;
            int digit;
            for(int i=1; i<=cardNumber.Length; i++)
            {
                digit = int.Parse(cardNumber[cardNumber.Length - i].ToString());

                if (i % 2 == 0)
                {
                    digit = 2 * digit;
                }

                if(digit > 9)
                {
                    digit = (digit % 10) + 1;
                }

                sum += digit;
            }

            return sum % 10 == 0;
        }
    }
}

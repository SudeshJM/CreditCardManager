using Card.Domain.Model;
using Card.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CardAPI.Validators
{
    /// <summary>
    /// Extended validation for credit card details.
    /// </summary>
    public static class CreditCardValidator
    {

        /// <summary>
        /// Validates additional credit card details such as characters and luhn 10 check.
        /// </summary>
        /// <param name="creditCard">the credit card object</param>
        /// <returns>True if credit card details are valid else false.</returns>
        public static ValidationResult ValidateCreditCard(this CreditCard creditCard)
        {
            ValidationResult result = new ValidationResult();

            if (!Regex.IsMatch(creditCard.CardNumber, @"^[0-9]+$"))
            {
                result.Error = "The card number contains invalid characters.";
                return result;
            }

            if(!LuhnCheck(creditCard.CardNumber))
            {
                result.Error = "The card number is not valid.";
                return result;
            }

            result.IsSuccess = true;
            return result;
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

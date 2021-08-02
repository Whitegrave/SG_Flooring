using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.UI
{
    public class ConsoleIO
    {
        public static int GetIntFromUser(string prompt, bool allowZero = true, bool allowNegative = true, int maxNegative = -100, int maxPositive = 100, bool ClearLog = false)
        {
            string userInput;
            int result;
            bool inputValid = false;
            do
            {
                if (ClearLog)
                    Console.Clear();

                // Display the prompt to inform user of desired input
                Console.Write(prompt);
                userInput = Console.ReadLine();
                bool bParsed = int.TryParse(userInput, out result);
                // Validate and proceed to next loop if invalid
                if (!bParsed || !allowZero && result == 0 || !allowNegative && result < 0 || result < maxNegative || result > maxPositive)
                {
                    Console.WriteLine("\nThat is not a valid input.");
                    continue;
                }
                // User input passed all specified criteria
                inputValid = true;
            } while (!inputValid);
            return result;
        }

        public static decimal GetDecimalFromUser(string prompt, bool allowZero = true, bool allowNegative = true, int maxNegative = -100, int maxPositive = 100, bool ClearLog = false)
        {
            string userInput;
            decimal result;
            bool inputValid = false;
            do
            {
                if (ClearLog)
                    Console.Clear();

                // Display the prompt to inform user of desired input
                Console.Write(prompt);
                userInput = Console.ReadLine();
                bool bParsed = decimal.TryParse(userInput, out result);
                // Validate and proceed to next loop if invalid
                if (!bParsed || !allowZero && result == 0 || !allowNegative && result < 0 || result < maxNegative || result > maxPositive)
                {
                    Console.WriteLine("\nThat is not a valid input.");
                    continue;
                }
                // User input passed all specified criteria
                inputValid = true;
            } while (!inputValid);
            return result;
        }

        public static string GetStringFromUser(string prompt, int minLength = 1, int maxLength = 100, bool allowLettersOnly = true, bool allowSpaces = false, bool allowNumbers = false, bool allowExoticCharacters = false, bool allowEmptyString = false, bool allowOverrideAllowAny = false, bool ClearLog = false)
        {
            string userInput;
            bool inputValid = false;
            do
            {
                if (ClearLog)
                    Console.Clear();

                // Display the prompt to inform user of desired input
                Console.Write(prompt);
                userInput = Console.ReadLine().Trim();
                // Quickly return without computation if override or empty string was opted in
                if (allowOverrideAllowAny || allowEmptyString && userInput == "")
                    return userInput;

                // Quickly move to next loop if length is out of range, to prevent unnecessary calculations
                if (userInput.Length > maxLength || userInput.Length < minLength)
                {
                    Console.WriteLine("\nThat is not a valid input.");
                    continue;
                }

                // Cycle through the string to qualify various criteria
                bool containsLettersOrSpacesOnly = userInput.All(c => Char.IsLetter(c) || Char.IsWhiteSpace(c));
                bool containsNumbers = userInput.Any(c => Char.IsNumber(c));

                // Validate and proceed to next loop if invalid
                if (((!allowSpaces || allowLettersOnly) && userInput.Contains(" ")) || (!allowNumbers || allowLettersOnly) && containsNumbers)
                {
                    Console.WriteLine("\nThat is not a valid input.");
                    continue;
                }

                // Final check saved for last due to being a heavy calculation, searching for symbols
                bool containsExotics = userInput.Any(c => (!Char.IsNumber(c) && !Char.IsLetter(c) && !Char.IsWhiteSpace(c)));
                if (containsExotics && !allowExoticCharacters)
                {
                    Console.WriteLine("\nThat is not a valid input.");
                    continue;
                }

                // User input passed all specified criteria
                inputValid = true;
            } while (!inputValid);
            return userInput;
        }

        public static void DisplayToUser(string prompt, bool WaitForKey = false, bool ClearLog = false)
        {
            Console.WriteLine(prompt);
            if (WaitForKey)
                Console.ReadKey();

            if (ClearLog)
                Console.Clear();
        }
    }
}

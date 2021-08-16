using SG_Flooring.BLL;
using SG_Flooring.Models.Responses;
using SG_Flooring.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Workflows
{
    public class DelOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            CheckDateResponse dateResponse = new CheckDateResponse();
            string userInput = "";
            int orderNumber = -1;

            // Get a valid date from user
            while (!dateResponse.Success)
            {
                userInput = ConsoleIO.GetStringFromUser("-----------------------------\n" +
                                                                   "Remove an Order\n" +
                                                                   "-----------------------------\n\n" +
                                                                   "Q to return to main menu\n" +
                                                                   "Enter Date in format ##/##/####:\n", 1, 10, false, false, true, true, false, false, true).ToUpper();
                // Escape string
                if (userInput == "Q")
                    return;

                // Determine if provided date was invalid and inform user
                dateResponse = manager.ValidateDate(userInput);
                if (!dateResponse.Success)
                {
                    ConsoleIO.DisplayToUser(dateResponse.Message, true, true);
                }
            }

            // Get a valid order number from user
            GetOrderResponse orderResponse = new GetOrderResponse();
            while (!orderResponse.Success)
            {
                userInput = ConsoleIO.GetStringFromUser($"{dateResponse.Date}\n\n" +
                                                         "Q to return to main menu\n" +
                                                         "Enter Order Number: ", 1, 4, false, false, true, false, false, false, true).ToUpper();

                // Escape string
                if (userInput == "Q")
                    return;

                // Attempt to parse input into order int
                bool bParse = int.TryParse(userInput, out orderNumber);

                if (!bParse)
                {
                    ConsoleIO.DisplayToUser("Invalid number.");
                    continue;
                }

                // Check for existing order
                orderResponse = manager.LookupOrder(dateResponse.Date, orderNumber);

                // Determine if order was found and inform user
                if (!orderResponse.Success)
                {
                    ConsoleIO.DisplayToUser(orderResponse.Message, true, true);
                }
            }
            ConsoleIO.DisplayOrderDetails(orderResponse.Order);

            // Confirm to delete
            string confirmOrder = ConsoleIO.GetStringFromUser("\n\nRemove this order? Y to confirm, any other key to quit...", 1, 1, false, true, true, true, false, false, false).ToUpper();

            // Abort if not confirmed
            if (confirmOrder != "Y")
                return;

            manager.DeleteOrder(orderResponse.Order);
            ConsoleIO.DisplayToUser("The order was removed.", true);
        }
    }
}

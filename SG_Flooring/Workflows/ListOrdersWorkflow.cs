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
    class ListOrdersWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            string orderDate = ConsoleIO.GetStringFromUser("-----------------------------\n" +
                                                               "Lookup an Order\n" +
                                                               "-----------------------------\n\n" +
                                                               "Q to return to main menu" +
                                                               "Enter Date in format ##/##/####:\n", 1, 5, false, false, true, false, false, false, true);
            GetOrderResponse response = manager.GetOrder(orderDate, orderNumber);

            if (response.Success)
            {
                ConsoleIO.DisplayAccountDetails(response.Account);
            }
            else
            {
                ConsoleIO.DisplayToUser("The following error occurred: " + response.Message, false);
            }
            ConsoleIO.DisplayToUser("\nPress any key to continue...", true);
        }
    }
}

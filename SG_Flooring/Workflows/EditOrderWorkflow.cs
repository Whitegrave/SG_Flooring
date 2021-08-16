using SG_Flooring.BLL;
using SG_Flooring.Models;
using SG_Flooring.Models.Responses;
using SG_Flooring.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Workflows
{
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            CheckDateResponse dateResponse = new CheckDateResponse();
            Order editOrder = new Order();
            List<State> states = manager.GetStatesFromRepo();
            List<Product> prods = manager.GetProductsFromRepo();
            string userInput = "";
            int orderNumber = -1;

            // Get a valid date from user
            while (!dateResponse.Success)
            {
                userInput = ConsoleIO.GetStringFromUser("-----------------------------\n" +
                                                                   "Edit an Order\n" +
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
            editOrder = orderResponse.Order;

            // Begin prompting for changes

            // Prompt for Customer Name
            string newName = ConsoleIO.GetStringFromUser("Enter customer name: ", 1, 30, false, true, true, true, true, false, true);
            if (newName == "")
                newName = editOrder.Customer;

            editOrder.Customer = newName;

            // List States
            ConsoleIO.DisplayToUser("\nOrder found. Enter selected changes...\n");
            ConsoleIO.DisplayToUser("Available States:\n");
            foreach (State item in states)
            {
                ConsoleIO.DisplayToUser($"{states.IndexOf(item) + 1}. {item.Initials}, {item.Name}\n");
            }

            // Get State [string of [states(index).initials]]
            int newState = ConsoleIO.GetIntFromUser("\nEnter the number for customer's state: ", false, false, 0, states.Count(), false, true);
            if (newState != 0)
            {
                editOrder.State = states[newState - 1].Initials;
            }
            
            // List Products
            ConsoleIO.DisplayToUser("Available Products:\n");
            foreach (Product item in prods)
            {
                ConsoleIO.DisplayToUser($"{prods.IndexOf(item) + 1}. {item.Name}, {item.CostSqf} per sqf, {item.CostLaborSqf} labor per sqf\n");
            }

            // Get Product
            int newProd = ConsoleIO.GetIntFromUser("\nEnter the number for desired product: ", false, false, 0, prods.Count(), false, true);
            if (newProd != 0)
            {
                editOrder.Product = prods[newProd - 1].Name;
            }

            // Get Area
            decimal newArea = 0.00M;
            while (newArea < 50M)
            {
                newArea = ConsoleIO.GetDecimalFromUser("\nEnter area of job (sqf) min 50.00: ", false, false, 0, 10000, false, true);
                if (newArea == 0)
                {
                    newArea = editOrder.Area;
                }                
            }
            editOrder.Area = newArea;

            // Calcualte Total
            editOrder.TaxRate = states[states.IndexOf(states.First(x => x.Initials == editOrder.State))].TaxRate;
            editOrder.CostPSqf = prods[prods.IndexOf(prods.First(x => x.Name == editOrder.Product))].CostSqf;
            editOrder.LaborPSqf = prods[prods.IndexOf(prods.First(x => x.Name == editOrder.Product))].CostLaborSqf;
            editOrder.MaterialCostTotal = editOrder.CostPSqf * editOrder.Area;
            editOrder.LaborTotal = editOrder.LaborPSqf * editOrder.Area;
            editOrder.TaxTotal = (editOrder.MaterialCostTotal + editOrder.LaborTotal) * (editOrder.TaxRate / 100);
            editOrder.Total = editOrder.MaterialCostTotal + editOrder.LaborTotal + editOrder.Total;

            // Summarize for user
            ConsoleIO.DisplayOrderDetails(editOrder);

            // Prompt to add Order Y/N
            string confirmOrder = ConsoleIO.GetStringFromUser("\n\nConfirm changes to this order? Y to confirm, any other key to quit...", 1, 1, false, true, true, true, false, false, false).ToUpper();

            // Abort if not confirmed
            if (confirmOrder != "Y")
                return;

            Response saveResponse = manager.SaveOrder(editOrder);

            // Inform user
            if (saveResponse.Success == false)
            {
                ConsoleIO.DisplayToUser($"The order could not be saved due to the following error: \n{saveResponse.Message}\nPress any key to return to main menu...");
            }

            ConsoleIO.DisplayToUser("The order was saved. Press any key to return to main menu...", true);
        }
    }
}

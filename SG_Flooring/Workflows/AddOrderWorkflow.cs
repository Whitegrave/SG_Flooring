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
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            CheckDateResponse dateResponse = new CheckDateResponse();
            Order newOrder = new Order();
            List<State> states = manager.GetStatesFromRepo();
            List<Product> prods = manager.GetProductsFromRepo();

            // Prompt for Customer Name
            newOrder.Customer = ConsoleIO.GetStringFromUser("Enter customer name: ", 1, 30, false, true, true, true, false, false, true);

            // Prompt for Date
            while (!dateResponse.Success)
            {
                string validDate = ConsoleIO.GetStringFromUser("Enter a date in ##/##/#### format:", 8, 10, false, false, true, true, false, true, true);
                dateResponse = manager.ValidateDate(validDate);
                if (dateResponse.Success)
                {
                    newOrder.Date = dateResponse.Date;
                }
            }

            // List States
            ConsoleIO.DisplayToUser("Available States:\n");
            foreach (State item in states)
            {
                ConsoleIO.DisplayToUser($"{states.IndexOf(item) + 1}. {item.Initials}, {item.Name}\n");
            }

            // Get State [string of [states(index).initials]]
            newOrder.State = states[ConsoleIO.GetIntFromUser("\nEnter the number for customer's state: ", false, false, 0, states.Count(), false) - 1].Initials;

            // List Products
            ConsoleIO.DisplayToUser("Available Products:\n");
            foreach (Product item in prods)
            {
                ConsoleIO.DisplayToUser($"{prods.IndexOf(item) + 1}. {item.Name}, {item.CostSqf} per sqf, {item.CostLaborSqf} labor per sqf\n");
            }

            // Get Product
            newOrder.Product = prods[ConsoleIO.GetIntFromUser("\nEnter the number for desired product: ", false, false, 0, prods.Count(), false) - 1].Name;

            // Get Area
            while (newOrder.Area < 50M)
            {
                newOrder.Area = ConsoleIO.GetDecimalFromUser("\nEnter area of job (sqf) min 50.00: ", false, false, 0, 10000, false);
            }

            // Calcualte Total
            newOrder.TaxRate = states[states.IndexOf(states.First(x => x.Initials == newOrder.State))].TaxRate;
            newOrder.CostPSqf = prods[prods.IndexOf(prods.First(x => x.Name == newOrder.Product))].CostSqf;
            newOrder.LaborPSqf = prods[prods.IndexOf(prods.First(x => x.Name == newOrder.Product))].CostLaborSqf;
            newOrder.MaterialCostTotal = newOrder.CostPSqf * newOrder.Area;
            newOrder.LaborTotal = newOrder.LaborPSqf * newOrder.Area;
            newOrder.TaxTotal = (newOrder.MaterialCostTotal + newOrder.LaborTotal) * (newOrder.TaxRate / 100);
            newOrder.Total = newOrder.MaterialCostTotal + newOrder.LaborTotal + newOrder.Total;

            // Summarize for user
            ConsoleIO.DisplayOrderDetails(newOrder);

            // Prompt to add Order Y/N
            string confirmOrder = ConsoleIO.GetStringFromUser("\n\nConfirm this order? Y to confirm, any other key to quit...", 1, 1, false, true, true, true, false, false, false).ToUpper();

            // Abort if not confirmed
            if (confirmOrder != "Y")
                return;

            // Write to file
            manager.SaveOrder(newOrder);

        }
    }
}

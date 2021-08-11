using SG_Flooring.BLL;
using SG_Flooring.Models.Responses;
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
            string userInput = "";
            int orderNumber = -1;

            // Prompt for Customer Name
            
            // List States

            // Get State

            // List Products

            // Get Product

            // Get Area

            // Calcualte Total

            // Summarize for user

            // Prompt to add Order Y/N

            // Write to file

        }
    }
}

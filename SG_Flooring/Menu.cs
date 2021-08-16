using SG_Flooring.Data;
using SG_Flooring.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.UI
{
    public static class Menu
    {
        public static object DespositWorkflow { get; private set; }

        public static void Start()
        {
            // Instantiate product database
            ProductRepo ProdRepo = new ProductRepo();

            while (true)
            {
                string userChoice = ConsoleIO.GetStringFromUser("SG Flooring Application\n" +
                                                                "----------------------------\n" +
                                                                "1. List Orders\n" +
                                                                "2. Add an Order\n" +
                                                                "3. Edit an Order\n" +
                                                                "4. Remove an Order\n\n" +
                                                                "Q to quit\n" +
                                                                "----------------------------\n\n" +
                                                                "Enter selection: ", 1, 1, false, false, true, false, false, false, true).ToUpper();

                switch (userChoice)
                {
                    case "1":
                        ListOrdersWorkflow listWorkflow = new ListOrdersWorkflow();
                        listWorkflow.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow addWorkflow = new AddOrderWorkflow();
                        addWorkflow.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow editWorkflow = new EditOrderWorkflow();
                        editWorkflow.Execute();
                        break;
                    case "4":
                        DelOrderWorkflow delWorkflow = new DelOrderWorkflow();
                        delWorkflow.Execute();
                        break;
                    case "Q":
                        return;
                    default:
                        break;
                }
            }
        }
    }
}

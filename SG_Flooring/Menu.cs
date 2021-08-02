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
            // init hard file repository
            //FileAccountRepository.InitializeFileRepo();

            while (true)
            {
                string userChoice = ConsoleIO.GetStringFromUser("SG Flooring Application\n" +
                                                                "----------------------------\n" +
                                                                "1. List Orders\n" +
                                                                "2. Add an Order\n" +
                                                                "3. Edit an Order\n" +
                                                                "4. Remove an Order\n\n" +
                                                                "R to re-create file repository from default values\n\n" +
                                                                "Q to quit\n" +
                                                                "----------------------------\n\n" +
                                                                "Enter selection: ", 1, 1, false, false, true, false, false, false, true).ToUpper();

                switch (userChoice)
                {
                    case "1":
                        //AccountLookupWorkflow lookupWorkflow = new AccountLookupWorkflow();
                        //lookupWorkflow.Execute();
                        break;
                    case "2":
                        //DepositWorkflow depositWorkflow = new DepositWorkflow();
                        //depositWorkflow.Execute();
                        break;
                    case "3":
                        //WithdrawWorkflow withdrawWorkflow = new WithdrawWorkflow();
                        //withdrawWorkflow.Execute();
                        break;
                    case "4":
                        //WithdrawWorkflow withdrawWorkflow = new WithdrawWorkflow();
                        //withdrawWorkflow.Execute();
                        break;
                    case "R":
                        //FileAccountRepository.CreateLiveRepo();
                        //ConsoleIO.DisplayToUser("\nRepository reset. Press any key to continue...", true, true);
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

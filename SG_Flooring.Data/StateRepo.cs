using SG_Flooring.Models;
using SG_Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Data
{
    public class StateRepo : IStateRepo
    {
        private List<State> _states = new List<State>();

        // Constructor populates state list
        public StateRepo()
        {
            // Attempt to find and load states file
            try
            {
                string[] rows = File.ReadAllLines(@".\Taxes.txt");
                // Remove header row
                rows = rows.Skip(1).ToArray();

                for (int i = 0; i < rows.Length; i++)
                {
                    // Parse delimited lines into items
                    string[] fields = rows[i].Split(',');
                    State x = new State();

                    // Populate State fields
                    x.Initials = fields[0];
                    x.Name = fields[1];
                    x.TaxRate = Decimal.Parse(fields[2]);
                    // Add built state to repo
                    _states.Add(x);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error was encountered parsing state and tax file data: \n");
                Console.WriteLine(String.Concat(e.Message, e.StackTrace));
                _states = null;
                Console.ReadKey();
            }
        }

        public List<State> GetStates()
        {
            return _states;
        }
    }
}

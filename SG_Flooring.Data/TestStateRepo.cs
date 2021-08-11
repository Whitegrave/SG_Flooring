using SG_Flooring.Models;
using SG_Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.Data
{
    public class TestStateRepo : IStateRepo
    {
        private List<State> _states;

        // Constructor to populate test data
        public TestStateRepo()
        {
            _states.Add(new State { Initials = "OH", Name = "Ohio", TaxRate = 6.25M });
            _states.Add(new State { Initials = "PA", Name = "Pennsylvania", TaxRate = 6.75M });
            _states.Add(new State { Initials = "MI", Name = "Michigan", TaxRate = 5.75M });
            _states.Add(new State { Initials = "IN", Name = "Indiana", TaxRate = 6.00M });
        }
    }
}

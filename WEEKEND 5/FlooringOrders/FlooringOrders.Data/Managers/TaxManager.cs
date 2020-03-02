using FlooringOrders.Models.Interfaces;
using FlooringOrders.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data.Managers
{
    public class TaxManager : ITaxHandler
    {
        private ITaxHandler Handler;
        public TaxManager(ITaxHandler handler)
        {
            Handler = handler;
        }

        public Tax RetrieveOne(string stateAbbreviation)
        {
            return Handler.RetrieveOne(stateAbbreviation);
        }

        public List<Tax> RetrieveAll()
        {
            return Handler.RetrieveAll();
        }

        public Tax RetrieveOneByFull(string state)
        {
            return Handler.RetrieveOneByFull(state);
        }
    }
}

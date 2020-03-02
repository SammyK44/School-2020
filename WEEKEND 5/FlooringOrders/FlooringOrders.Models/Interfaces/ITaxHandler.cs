using FlooringOrders.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Models.Interfaces
{
    public interface ITaxHandler
    {
        Tax RetrieveOne(string stateAbbreviation);
        Tax RetrieveOneByFull(string state);
        List<Tax> RetrieveAll();
    }
}

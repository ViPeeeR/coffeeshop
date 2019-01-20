using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShops.Common
{
    public enum StatusPayment
    {
        Wait = 0,
        Paid,
        Cancel
    }

    public enum StatusOrder
    {
        WaitPayment = 0,
        Prepares,
        Ready
    }
}

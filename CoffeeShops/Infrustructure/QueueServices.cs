using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShops.Infrustructure
{
    public class QueueServices
    {
        public QueueTasks Shop { get; set; } = new QueueTasks();

        public QueueTasks Order { get; set; } = new QueueTasks();
    }
}

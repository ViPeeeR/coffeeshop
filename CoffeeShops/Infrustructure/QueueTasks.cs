using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoffeeShops.Infrustructure
{
    public class QueueTasks
    {
        private ConcurrentQueue<Func<Task>> _queue = new ConcurrentQueue<Func<Task>>();

        public bool Pending
        {
            get
            {
                return _queue.Count > 0;
            }
        }

        public void Add(Func<Task> action)
        {
            _queue.Enqueue(action);
        }

        public async Task<bool> Execute()
        {
            if (Pending)
            {
                if (!_queue.TryDequeue(out var action))
                    return false;

                try
                {
                    await action?.Invoke();
                    return true;
                }
                catch (HttpRequestException)
                {
                    _queue.Enqueue(action);
                }
            }

            return false;
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Superintendent
{
    public class Superintendent
    {
        private volatile bool _execute = true;
        private int _errorCode;
        private readonly Thread _thread;

        private readonly IProducerConsumerCollection<IRequest> _queue = new ConcurrentQueue<IRequest>();
        
        /// <summary>
        /// Our big gigantic lock. Playing it safe for now, just lock everything.
        /// </summary>
        private static readonly object _lock = new object();

        private void Stop()
        {
            lock (_lock)
            {
                _execute = false;
            }
        }

        private bool SafeGetExecuteValue()
        {
            lock (_lock)
            {
                return _execute;
            }
        }

        static bool SafeGetNextRequest(out IRequest value)
        {
            value = null;
            return false;
        }
        
        public Superintendent()
        {
            // Keep a handle to our originating thread
            _thread = Thread.CurrentThread;
        }

        public void Execute()
        {
            var spinWait = new SpinWait();
            var buffer = new List<IRequest>();

            do
            {
                // We spin one extra time at the beginning. Oh well.
                spinWait.SpinOnce();

                // The basic idea is a customizable pipeline with a
                // customizable walker, an object that accompanies 
                // the data through each phase of the pipeline. Data
                // sources can be other superintendents with the same
                // build and name. Superintendents can provide data
                // to one another. This way I could easily stream, for
                // example, Kinect data from my house to a different
                // machine for processing. The processed data could then request
                // a touch display filtered by location. A request floats in
                // the swarm until it can be handled. Any Superintendent can modify the pool
                // of requests. (How will I synchronize this...?) Each superintendent talks
                // to one another as equals but acts as king on the
                // machine. Superintendent is how I get all my computers
                // to interface together and share their resources.
                // Superintendents join together in a swarm, pool data
                // sources. One Superintendent can ask the swarm for
                // any type of data (Maybe based on tag?), get devices, etc.

                // Get and process a message, then continue
                IRequest nextRequest;
                while (_queue.TryTake(out nextRequest))
                {
                    buffer.Add(nextRequest);
                }

            } while (SafeGetExecuteValue());
        }

        public bool Ready
        {
            get { return true; }
        }
        
        public string Name
        {
            get { return "Superintendent"; }
        }
    }
}

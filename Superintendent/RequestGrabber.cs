using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Superintendent
{
    class RequestGrabber
    {
        private HttpListener _listener;
        
        public RequestGrabber()
        {
            _listener = new HttpListener();
        }
    }
}

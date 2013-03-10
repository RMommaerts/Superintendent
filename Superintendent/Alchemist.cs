using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Superintendent
{
    public class Alchemist : IAlchemist
    {
        public IEnumerable<IRequest> Transmute(IEnumerable<IRequest> ingredients)
        {
            foreach (IRequest request in ingredients)
            {
                yield return request;
            }
        }
    }
}

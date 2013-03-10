using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Superintendent
{
    public interface IAlchemist
    {
        IEnumerable<IRequest> Transmute(IEnumerable<IRequest> ingredients);
    }
}

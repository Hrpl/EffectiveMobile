using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveMobile.Domain.Exceptions;

public class NullParametrException : Exception
{
    public NullParametrException(string message) : base(message) { }
}

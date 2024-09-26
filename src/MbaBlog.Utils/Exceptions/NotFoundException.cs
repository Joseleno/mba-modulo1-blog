using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MbaBlog.Utils.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
}

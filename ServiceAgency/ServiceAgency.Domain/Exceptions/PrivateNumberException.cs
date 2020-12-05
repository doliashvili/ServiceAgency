using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Domain.Exceptions
{
    public class PrivateNumberException : Exception
    {
        public PrivateNumberException()
        {

        }
        public PrivateNumberException(string message) : base(message)
        {

        }
    }
}

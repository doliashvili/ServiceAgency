using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAgency.Domain.Exceptions
{
    public class DateTimeParseException : Exception
    {
        public DateTimeParseException()
        {

        }

        public DateTimeParseException(string message) : base(message)
        {

        }
    }
}

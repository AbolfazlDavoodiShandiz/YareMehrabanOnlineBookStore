using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Exceptions
{
    public class ApplicationDatabaseOperationException : Exception
    {
        public ApplicationDatabaseOperationException(Exception ex) : base("An exception occured in database operation.", ex)
        {
        }
    }
}

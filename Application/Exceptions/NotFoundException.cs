using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string entityName, object key) : base($"Entity {entityName} with Key {key} was Not Found!")
        {
            
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.Builders
{
    public class DataBaseBuilder
    {
        internal DataBaseContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            return new DataBaseContext(options);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsoleApp1
{
    class HumanContext:DbContext
    {
        public HumanContext():base("DbConnection")
            { }
        public DbSet<Human> Humans { get; set; }
    }
}

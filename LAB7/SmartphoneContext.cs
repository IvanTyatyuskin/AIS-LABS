using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LAB6
{
    class SmartphoneContext:DbContext
    {
        public SmartphoneContext(): base("DBConnection") { }

        public DbSet<Smartphone> Smartphones { get; set; }

        public DbSet<ProcessorManufacturer> ProcessorManufacturers { get; set; }
    }
}

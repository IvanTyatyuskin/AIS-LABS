using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LAB6
{
    class ProcessorManufacturer
    {
        [Key]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public ICollection<Smartphone> Smartphones { get; set; }

        public ProcessorManufacturer()
        {
            Smartphones = new List<Smartphone>();
        }
    }
}

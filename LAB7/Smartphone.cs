using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB6
{
    class Smartphone
    {
        public Guid ID { get; set; }

        public string Name { get; set; }
        public string Processor { get; set; }
        public string Ram { get; set; }
        public string Storage { get; set; }
        public string Price { get; set; }


        public Guid? processorManufacturerID { get; set; }

        public ProcessorManufacturer processorManufacturer { get; set; }


        public string GetInfo()
        {
            return $"Модель: {Name} \nПроцессор: {Processor} \nЦена: {Price} \n" +
                $"Оперативная память: {Ram} \nОбъем памяти: {Storage}";
        }
    }
}

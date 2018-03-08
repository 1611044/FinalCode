using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogWash.Models
{
    public class appointment
    {
        public int ID { get; set; }
        public DateTime apptime { get; set; }
        public string owner { get; set; }
        public string pet { get; set; }
        public string washtype { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DogWash.Models
{
    public class Wash
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string owner { get; set; }
        public string pet { get; set; }
        public string washtype { get; set; }

    }
}
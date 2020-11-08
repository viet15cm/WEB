using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBAPI.MVCModels
{
    public class mvcEmployee
    {
        [Required]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Addrees { get; set; }
        [Required]
        public string IDDE { get; set; }
    }
}
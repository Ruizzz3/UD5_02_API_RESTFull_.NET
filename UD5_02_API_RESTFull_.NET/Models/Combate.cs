using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UD5_02_API_RESTFull_.NET.Models
{
    public class Combate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}

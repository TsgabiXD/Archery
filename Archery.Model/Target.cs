using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archery.Model
{
    public class Target
    {
        public int Id { get; set; }
        
        public int ArrowNumber { get; set; }

        public int HittedArea { get; set; }
    }
}

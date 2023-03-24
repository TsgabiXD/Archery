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
        
        public int ArrowCount { get; set; }

        public int HitArea { get; set; }

        // foreign key 
        public User User { set; get; } = null!;
        public Event Event { set; get; } = null!;
    }
}

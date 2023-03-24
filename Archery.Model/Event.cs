using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archery.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;  
        public Parcour Parcour { get; set; }= null!;
        public bool IsRunning { get; set; }

    }
}

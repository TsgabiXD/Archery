using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archery.Model.ApiHelper
{
    public class NewTarget
    {
        public int ArrowCount { get; set; }

        public int HitArea { get; set; }

        public int EventId { get; set; }

        public int UserId { get; set; }
    }
}

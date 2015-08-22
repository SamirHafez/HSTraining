using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.Core.Models
{
    public class Cycle
    {
        public List<Workday> Workdays { get; set; }

        public bool Done
        {
            get
            {
                return Workdays.All(w => w.Done);
            }
        }

        public Workday this[int index]
        {
            get
            {
                return Workdays[index];
            }
        }

        public Cycle(List<Workday> workdays)
        {
            Workdays = workdays;
        }
    }
}

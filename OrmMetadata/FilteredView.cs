using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo
{
    class FilteredView : View
    {
        public View Source;
        public String FilterColumn;
        public String FilterValue;

        public override string getPhysicalName()
        {
            return Source.getPhysicalName();
        }
    }
}

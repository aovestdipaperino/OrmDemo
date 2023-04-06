using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo
{
    class Table : View
    {
        public String Name;
        public Table(String aName ) { this.Name = aName;  }

        public override string getPhysicalName()
        {
            return Name;
        }
    }
}

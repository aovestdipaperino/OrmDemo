using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo
{
    class MappedExtent
    {
        public String Name;
        public View View;
        public MappedType Type;

        public MappedExtent(String aName, View aView, MappedType aType) {
            this.Name = aName;
            this.View = aView;
            this.Type = aType;
        }
    }
}

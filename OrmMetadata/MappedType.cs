using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo
{
    internal class MappedType
    {
        public String Name;
        private Dictionary<String, MappedProperty> properties;

        public MappedType(String aName)
        {
            this.Name = aName;
            this.properties = new Dictionary<String, MappedProperty>();
        }

        public MappedType AddProperty(MappedProperty aProperty)
        {
            properties.Add(aProperty.Name, aProperty);
            return this;
        }

        public MappedProperty GetMappedProperty(String name)
        {
            return this.properties[name];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo
{
    public enum OrmType
    {
        STRING,
        NUMBER,
        COLLECTION
    }
    abstract class MappedProperty
    {
        public OrmType PropertyType;
        public String Name;

        public MappedProperty(String aName) { this.Name = aName; }
    }

    class ValueProperty : MappedProperty
    {
        public String columnName;
        public ValueProperty(String aPropertyName, String aColumnName, OrmType aPropertyType) : base(aPropertyName){ this.columnName = aColumnName; this.PropertyType = aPropertyType; }
    }

    class IdentityProperty : ValueProperty
    {
        public IdentityProperty(String aPropertyName, String aColumnName, OrmType aPropertyType) : base(aPropertyName, aColumnName, aPropertyType) { }
    }

    class CollectionProperty : MappedProperty
    {
        public String targetColumnName;
        public String targetExtent;
        public CollectionProperty(String aPropertyName, String aColumnName, String aTargetExtent) : base(aPropertyName)
        {
            this.targetColumnName = aColumnName;
            this.targetExtent = aTargetExtent;
        }
    }

}

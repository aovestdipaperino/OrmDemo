using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    internal class Property : Node
    {
        public String Name;
        public String TargetAlias;
        internal String physicalName;
        internal MappedType associatedType;
        public Property(String aName, String aTargetAlias = null) { this.Name = aName; this.TargetAlias = aTargetAlias; }

        public override void Accept(AstVisitor astVisitor)
        {
            astVisitor.VisitProperty(this);
        }

        internal String PhysicalName
        {
            get
            {
                return this.physicalName;
            }
            set
            {
                this.physicalName = value;
            }
        }

        internal MappedType AssociatedType
        {
            get
            {
                return this.associatedType;
            }
            set
            {
                this.associatedType = value;
            }
        }
    }
}

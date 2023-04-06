using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    internal class Exists : Node
    {
        public Property Property;
        public Node Filter;
        public String VariableName;
        public String TargetExtentName;

        public Exists(String aVariableName, Property aProperty, Node aFilter = null)
        {
            this.VariableName = aVariableName;
            this.Property = aProperty;
            this.Filter = aFilter;
        }

        public override void Accept(AstVisitor astVisitor)
        {
            astVisitor.VisitExists(this);
            // HACK: we rely on the fact that the only implementation of 
            // the visitor will handle the Exists properly.
            //if (this.Filter != null)
            //{
            //    this.Filter.Accept(astVisitor);
            //}
        }
    }
}

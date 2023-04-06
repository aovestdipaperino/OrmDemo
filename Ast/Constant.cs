using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    internal class Constant : Node
    {
        public Object Value;
        public OrmType Type;

        public Constant(int aValue) {
            Type = OrmType.NUMBER;
            Value = aValue; 
        }

        public Constant(String aValue)
        {
            Type = OrmType.STRING;
            Value = aValue;
        }

        public override void Accept(AstVisitor astVisitor)
        {
            astVisitor.VisitConstant(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    internal abstract class Node : IVisitable
    {
        public abstract void Accept(AstVisitor astVisitor);
    }
}

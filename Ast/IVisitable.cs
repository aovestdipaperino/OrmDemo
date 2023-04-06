using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    interface IVisitable
    {
        void Accept(AstVisitor astVisitor);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    public enum BinaryOperator
    {
        EQUAL,
        NOT_EQUAL,
        LIKE
    }

    internal class Binary : Node
    {
        public Node Left;
        public Node Right;
        public BinaryOperator binaryOperator;

        public Binary(Node aLeft, Node aRight, BinaryOperator aBinaryOperator)
        {
            this.Left = aLeft;
            this.Right = aRight;
            this.binaryOperator = aBinaryOperator;
        }

        public override void Accept(AstVisitor astVisitor)
        {
            Left.Accept(astVisitor);
            astVisitor.VisitBinary(this);
            Right.Accept(astVisitor);
        }
    }
}

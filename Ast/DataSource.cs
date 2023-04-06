using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    internal abstract class DataSource : IVisitable
    {
        public String Alias;
        private MappedType type;
        public DataSource(String anAlias) { this.Alias = anAlias; }
        public abstract void Accept(AstVisitor astVisitor);

        public MappedType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }

    internal class CollectionDataSource : DataSource
    {
        public Property Property;
        public CollectionDataSource(Property aProperty, String anAlias) : base(anAlias)
        {
            this.Property = aProperty;
        }

        public override void Accept(AstVisitor astVisitor)
        {
            astVisitor.VisitCollectionDataSource(this);
        }
    }
    internal class ExtentDataSource : DataSource
    {
        public String ExtentName;


        public ExtentDataSource(String anExtentName, String anAlias = null) : base(anAlias ?? anExtentName) {
            this.ExtentName = anExtentName;
        }

        public override void Accept(AstVisitor astVisitor)
        {
            astVisitor.VisitExtentDataSource(this);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    internal class AstVisitor
    {
        public virtual void VisitBinary(Binary binary) { }
        public virtual void VisitConstant(Constant contast) { }
        public virtual void VisitProperty(Property property) { }

        public virtual void VisitObject(String alias) { }

        public virtual void VisitRef(RefProjection alias) { }
        public virtual void VisitExtentDataSource(ExtentDataSource dataSource) { }

        public virtual void VisitCollectionDataSource(CollectionDataSource dataSource) { }

        public virtual void VisitExists(Exists exists) { }

        public virtual void EndVisitExists() { }
    }
}

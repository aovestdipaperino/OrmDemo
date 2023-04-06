using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    internal class Query : IVisitable
    {
        public ProjectionList Projection = new ProjectionList();
        public DataSource DataSource;
        public Node Filter;

        public Query(IEnumerable<ProjectionItem> someProjectionItems, DataSource aDataSource, Node aFilter = null)
        {
            foreach(var pi in someProjectionItems)
            {
                this.Projection.AddProjection(pi);
            }
            this.DataSource = aDataSource;
            this.Filter = aFilter;
        }

        public void Accept(AstVisitor astVisitor)
        {
            DataSource.Accept(astVisitor);
            if (Filter != null)
            {
                Filter.Accept(astVisitor);
            }
            Projection.Accept(astVisitor);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    internal abstract class ProjectionItem : IVisitable
    {
        public String Alias;
        public ProjectionItem(string anAlias) { this.Alias = anAlias; }

        public abstract void Accept(AstVisitor astVisitor);

    }
    /*
    class ObjectProjection : ProjectionItem
    {
        public ObjectProjection(string anAlias) : base(anAlias) { }
    }
    */

    class PropertyProjection : ProjectionItem
    {
        public Property Property;
        public PropertyProjection(Property aProperty, string anAlias) : base(anAlias)
        {
            this.Property = aProperty;
        }

        public override void Accept(AstVisitor astVisitor)
        {
            astVisitor.VisitProperty(this.Property);
        }
    }

    class ObjectProjection : ProjectionItem
    {
        public ObjectProjection(string anAlias) : base(anAlias) { }

        public override void Accept(AstVisitor astVisitor)
        {
            astVisitor.VisitObject(this.Alias);
        }
    }

    class RefProjection : ProjectionItem
    {
        public Property Id;
        public RefProjection(string anAlias) : base(anAlias) { }

        public override void Accept(AstVisitor astVisitor)
        {
            astVisitor.VisitRef(this);
            if (Id != null)
            {
                astVisitor.VisitProperty(this.Id);
            }
        }
    }

    internal class ProjectionList : IVisitable
    {
        public List<ProjectionItem> ProjectionItems = new List<ProjectionItem>();

        public ProjectionList AddProjection(ProjectionItem aProjectionItem)
        {
            ProjectionItems.Add(aProjectionItem);
            return this;
        }

        public void Accept(AstVisitor astVisitor)
        {
            foreach(var pi in ProjectionItems)
            {
                ((IVisitable)pi).Accept(astVisitor);
            }
        }
    }
}

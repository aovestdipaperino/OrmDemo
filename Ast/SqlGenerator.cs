using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    class SqlGenerator : AstVisitor
    {
        static SqlGenerator instance = new SqlGenerator();
        StringBuilder sb;
        static private Dictionary<BinaryOperator, String> binOpMap;

        private SqlGenerator()
        {
            binOpMap = new Dictionary<BinaryOperator, String>();
            binOpMap.Add(BinaryOperator.EQUAL, " = ");
            binOpMap.Add(BinaryOperator.NOT_EQUAL, " <> ");
            binOpMap.Add(BinaryOperator.LIKE, " LIKE ");
        }

        public void VisitLeafNode(Node n)
        {
            if (n is Constant)
            {
                VisitConstant(n as Constant);
                return;
            }
            if (n is Property)
            {
                VisitProperty(n as Property);
                return;
            }
            throw new NotImplementedException();
        }
        public override void VisitBinary(Binary binary)
        {
            sb.Append(binOpMap[binary.binaryOperator]);
        }

        public override void VisitConstant(Constant constant)
        {
            sb.Append(constant.Value.ToString());
        }

        public override void VisitProperty(Property property)
        {
            sb.Append(property.PhysicalName);
        }

        public override void VisitObject(string alias)
        {
            // HACK: this is a shortcut for DEMO purposes.
            sb.Append("*");
        }


        public override void VisitExtentDataSource(ExtentDataSource dataSource)
        {
            sb.Append(MetadataService.getViewForExtent(dataSource.ExtentName).getPhysicalName());
        }

        public override void VisitExists(Exists exists)
        {
            sb.Append("EXISTS( SELECT 1 FROM ");
            String targetTableName = MetadataService.getViewForExtent(exists.TargetExtentName).getPhysicalName();
            sb.Append(targetTableName);
            sb.Append("");
            sb.Append(" WHERE (");
            if (exists.Filter != null)
            {
                (exists.Filter as Binary).Accept(this);
                sb.Append(") AND (");
            }
            sb.Append(exists.Property.PhysicalName);
            sb.Append(" =");
            sb.Append(targetTableName);
            sb.Append(".");
            sb.Append((exists.Property.associatedType.GetMappedProperty(exists.Property.Name) as CollectionProperty).targetColumnName);
            sb.Append("))");
        }

        public static String generateSql(Query query)
        {
            BindingVisitor bv = new BindingVisitor();
            query.Accept(bv);
            instance.sb = new StringBuilder();
            instance.sb.Append("SELECT ");
            var previousPiNull = true;
            foreach (ProjectionItem pi in query.Projection.ProjectionItems)
            {
                if (!previousPiNull) instance.sb.Append(", ");
                pi.Accept(instance);

                previousPiNull = false;
            }
            instance.sb.Append(" FROM ");
            query.DataSource.Accept(instance);

            if (query.Filter != null)
            {
                instance.sb.Append(" WHERE (");
                query.Filter.Accept(instance);
                instance.sb.Append(")");
            }

            return instance.sb.ToString();
        }
    }
}

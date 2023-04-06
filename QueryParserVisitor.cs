using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo
{

    class Program
    {
        public static MappedExtent CreateOrdersExtent()
        {

            MappedType orderType = new MappedType("Order");

            orderType
                .AddProperty(new IdentityProperty("Id", "[OID]", OrmType.NUMBER))
                .AddProperty(new ValueProperty("Customer", "[CUSTOMER_ID]", OrmType.NUMBER))
                .AddProperty(new ValueProperty("Date", "[DATE]", OrmType.STRING))
                .AddProperty(new ValueProperty("Destination","[DESTINATION]", OrmType.STRING))
                .AddProperty(new ValueProperty("Total","[TOTAL]", OrmType.NUMBER));

            return new MappedExtent("_Orders_", new Table("[ORDERS]"), orderType);
        }

        public static MappedExtent CreateCustomersExtent()
        {
            MappedType customerType = new MappedType("Customer");

            customerType
                .AddProperty(new IdentityProperty("Id", "[ID]", OrmType.NUMBER))
                .AddProperty(new ValueProperty("Name", "[NAME]", OrmType.STRING))
                .AddProperty(new ValueProperty("Location", "[LOCATION]", OrmType.STRING))
                .AddProperty(new CollectionProperty("Orders", "[CUSTOMER_ID]", "_Orders_"));

            return new MappedExtent("_Customers_", new Table("[CUSTOMERS]"), customerType);
        }

        public static void CreateMetadata()
        {
            MetadataService.addExtent(CreateCustomersExtent());
            MetadataService.addExtent(CreateOrdersExtent());
        }

        static void CompileQueryToSql(string exampleName, string query)
        {
            Console.WriteLine("============ " + exampleName + ": '" + query + "' ==========");
            SQLParser parser = new SQLParser(new CommonTokenStream(new SQLLexer(new AntlrInputStream(query))));
            Console.WriteLine(Ast.SqlGenerator.generateSql(new QueryParserVisitor().VisitQuery(parser.query())));
            Console.WriteLine();
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            CreateMetadata();
            CompileQueryToSql("Example I", "SELECT OBJECT(c) FROM _Customers_ AS c WHERE c.Name = 'Alpha' ");
            CompileQueryToSql("Example II","SELECT REF(c) FROM _Customers_ AS c WHERE c.Name LIKE 'A*' ");
            CompileQueryToSql("Example III", "SELECT c.Id, c.Name FROM _Customers_ AS c WHERE EXISTS(o IN c.Orders WHERE o.Destination = c.Location)");
        }
    }
}

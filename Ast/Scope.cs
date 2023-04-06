using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    class Scope
    {
        Dictionary<String, DataSource> localScope = new Dictionary<String, DataSource>();
        public Scope parentScope = null;

        public Scope(Scope aParentScope = null)
        {
            this.parentScope = aParentScope;
        }

        public void Add(String anAlias, DataSource aDataSource)
        {
            this.localScope.Add(anAlias, aDataSource);
        }

        public DataSource Resolve(String anAlias)
        {
            if (anAlias == null)
            {
                if (this.localScope.Count == 1)
                {
                    // TODO: 
                    return localScope.Values.First();
                }
                return null;
            }
            DataSource result = null;
            if (localScope.TryGetValue(anAlias, out result))
            {
                return result;
            }

            return parentScope == null ? null : parentScope.Resolve(anAlias);
        }
    }
}

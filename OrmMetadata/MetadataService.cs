using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo
{
    class MetadataService
    {
        private static Dictionary<String, MappedExtent> mappedViews = new Dictionary<string, MappedExtent>();

        public static void addExtent(MappedExtent extent) { mappedViews.Add(extent.Name, extent); }
        public static View getViewForExtent(String extentName) { 
            // It's fine to throw NPE in demo code.
            return mappedViews[extentName].View; 
        }

        public static MappedType getTypeForExtent(String extentName)
        {
            return mappedViews[extentName].Type;
        }
    }
}

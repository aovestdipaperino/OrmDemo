using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmDemo.Ast
{
    class BindingVisitor : AstVisitor
    {
        Scope localScope = new Scope();

        public override void VisitExtentDataSource(ExtentDataSource dataSource)
        {
            MappedType type = MetadataService.getTypeForExtent(dataSource.ExtentName);
            if (type == null)
            {
                throw new ArgumentException("METADATA: Cannot resolve extent: " + dataSource.ExtentName);
            }
            dataSource.Type = type;

            localScope.Add(dataSource.Alias, dataSource);
        }

        public override void VisitCollectionDataSource(CollectionDataSource dataSource)
        {
            var extentName = (localScope.Resolve(dataSource.Property.TargetAlias).Type.GetMappedProperty(dataSource.Property.Name) as CollectionProperty).targetExtent;
            MappedType type = MetadataService.getTypeForExtent(extentName);
            if (type == null)
            {
                throw new ArgumentException("METADATA: Cannot resolve extent: " + extentName);
            }
            dataSource.Type = type;

            //localScope.Add(dataSource.Alias, dataSource);
        }

        public override void VisitExists(Exists exists)
        {
            // PUSH the new scope
            localScope = new Scope(localScope);
            var collDataSource = new CollectionDataSource(exists.Property, exists.VariableName);

            localScope.Add(exists.VariableName,collDataSource);
            collDataSource.Accept(this);
            exists.Property.Accept(this);
            if (exists.Filter != null)
            {
                exists.Filter.Accept(this);
            }
            var collectionProperty = localScope.Resolve(exists.Property.TargetAlias).Type.GetMappedProperty(exists.Property.Name) as CollectionProperty;
            exists.TargetExtentName = collectionProperty.targetExtent;
        }

        public override void EndVisitExists()
        {
            localScope = localScope.parentScope;
        }

        public override void VisitRef(RefProjection refProjection)
        {
            DataSource targetSource = localScope.Resolve(refProjection.Alias);

            if (targetSource == null)
            {
                throw new ArgumentException("METADATA: Cannot resolve REF: " + refProjection.Alias);
            }

            refProjection.Id = new Property("Id", refProjection.Alias);
        }

        public override void VisitProperty(Property property)
        {
            DataSource targetSource = localScope.Resolve(property.TargetAlias);

            if (targetSource == null)
            {
                throw new ArgumentException("METADATA: Cannot resolve property: " + property.Name);
            }
            property.AssociatedType = targetSource.Type;
            var mappedProperty = targetSource.Type.GetMappedProperty(property.Name);
            if (mappedProperty == null)
            {
                throw new ArgumentException("METADATA: Cannot resolve property: " + property.Name);
            }
            String columnName = null;

            if (mappedProperty is ValueProperty)
            {
                columnName = (mappedProperty as ValueProperty).columnName;
            } else if (mappedProperty is CollectionProperty)
            {
                columnName = (property.AssociatedType.GetMappedProperty("Id") as IdentityProperty).columnName;
            } else
            {
                columnName = (targetSource.Type.GetMappedProperty("Id") as IdentityProperty).columnName;
            }
            if (targetSource is ExtentDataSource)
            {
                property.PhysicalName = 
                    MetadataService.getViewForExtent((targetSource as ExtentDataSource).ExtentName).getPhysicalName() +
                    "." + columnName;
                return;
            } 
            if (targetSource is CollectionDataSource)
            {
                var collectionProperty = (localScope.Resolve("o") as CollectionDataSource).Property.AssociatedType.GetMappedProperty((localScope.Resolve("o") as CollectionDataSource).Property.Name) as CollectionProperty;

                property.PhysicalName = 
                    MetadataService.getViewForExtent(collectionProperty.targetExtent).getPhysicalName() +
                    "." + columnName;
                return;
            }

            throw new NotImplementedException();

        }
    }
}

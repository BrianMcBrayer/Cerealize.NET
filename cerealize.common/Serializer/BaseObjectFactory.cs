using System;
using System.Collections.Generic;
using cerealize.common.Objects.Contracts;
using cerealize.common.Objects.Contracts.Strings;
using cerealize.common.Serializer.Contracts;

namespace cerealize.common.Serializer
{
    public abstract class BaseObjectFactory : IObjectFactory
    {
        private readonly Dictionary<Type, Func<IBaseObject>> _creationMappings = new Dictionary<Type, Func<IBaseObject>>();

        public bool CanCreateObject(Type t)
        {
            return _creationMappings.ContainsKey(t);
        }
        public object CreateObject(Type t)
        {
            if (!CanCreateObject(t))
            {
                throw new Exception(string.Format("Object type not supported for creation {0}", t.FullName));
            }

            return _creationMappings[t].Invoke();
        }

        protected void RegisterFactoryType(Type t, Func<IBaseObject> createFn)
        {
            _creationMappings.Add(t, createFn);
        }
    }
}
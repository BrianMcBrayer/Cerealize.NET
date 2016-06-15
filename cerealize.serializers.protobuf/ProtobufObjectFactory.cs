using System;
using System.Collections.Generic;
using cerealize.common.Objects.Contracts;
using cerealize.common.Objects.Contracts.Strings;
using cerealize.common.Serializer.Contracts;
using cerealize.serializers.protobuf.Objects.Strings;

namespace cerealize.serializers.protobuf
{
    public class ProtobufObjectFactory : IObjectFactory
    {
        private readonly Dictionary<Type, Func<IBaseObject>> _creationMappings = new Dictionary<Type, Func<IBaseObject>>
        {
            {typeof (ISimpleString), CreateSimpleString},
            {typeof(IDuplexString), CreateDuplexString}
        };

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

        private static ProtobufSimpleString CreateSimpleString()
        {
            return new ProtobufSimpleString
            {
                FirstName = "Bentworthy"
            };
        }

        private static ProtobufDuplexString CreateDuplexString()
        {
            return new ProtobufDuplexString
            {
                FirstName = "Bentworthy",
                LastName = "Smith"
            };
        }
    }
}
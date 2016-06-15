using System;
using System.Collections.Generic;
using cerealize.common.Objects.Contracts;
using cerealize.common.Objects.Contracts.Strings;
using cerealize.common.Serializer;
using cerealize.serializers.protobuf.Objects.Strings;

namespace cerealize.serializers.protobuf
{
    public class ProtobufObjectFactory : BaseObjectFactory
    {
        public ProtobufObjectFactory()
        {
            RegisterFactoryType(typeof(ISimpleString), CreateSimpleString);
            RegisterFactoryType(typeof(IDuplexString), CreateDuplexString);
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
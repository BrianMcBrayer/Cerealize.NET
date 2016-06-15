using System;
using System.IO;
using cerealize.common.Objects.Contracts;
using cerealize.common.Objects.Contracts.Strings;
using cerealize.common.Serializer.Contracts;
using cerealize.serializers.protobuf.Objects.Strings;

namespace cerealize.serializers.protobuf
{
    public class ProtobufSerializer : ISerializer<ProtobufObjectFactory>
    {
        public ProtobufSerializer()
        {
            ProtoBuf.Serializer.PrepareSerializer<ProtobufSimpleString>();
            ProtoBuf.Serializer.PrepareSerializer<ProtobufDuplexString>();
        }

        public byte[] Serialize(IBaseObject obj)
        {
            using (var stream = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        public IBaseObject Deserialize(Type t, byte[] obj)
        {
            using (var stream = new MemoryStream(obj))
            {
                if (t.IsAssignableFrom(typeof(ISimpleString)))
                {
                    return ProtoBuf.Serializer.Deserialize<ProtobufSimpleString>(stream);
                }
                if (t.IsAssignableFrom(typeof (IDuplexString)))
                {
                    return ProtoBuf.Serializer.Deserialize<ProtobufDuplexString>(stream);
                }
            }

            throw new NotImplementedException();
        }

        public ProtobufObjectFactory GetFactory()
        {
            return new ProtobufObjectFactory();
        }
    }
}

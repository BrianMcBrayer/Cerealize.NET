using System;
using System.Collections.Generic;
using Bond;
using Bond.IO.Unsafe;
using Bond.Protocols;
using cerealize.common.Objects.Contracts;
using cerealize.common.Objects.Contracts.Strings;
using cerealize.common.Serializer.Contracts;
using cerealize.serializers.bond.Objects.Strings;

namespace cerealize.serializers.bond
{
    public class BondSerializer : ISerializer<BondObjectFactory>
    {
        private readonly Dictionary<Type, Serializer<CompactBinaryWriter<OutputBuffer>>> _serializers = new Dictionary<Type, Serializer<CompactBinaryWriter<OutputBuffer>>>();
        private readonly Dictionary<Type,
            Deserializer<CompactBinaryReader<InputBuffer>>> _deserializers = new Dictionary<Type, Deserializer<CompactBinaryReader<InputBuffer>>>();

        public BondSerializer()
        {
            RegisterType(typeof(BondSimpleString), typeof (ISimpleString));
            RegisterType(typeof (BondDuplexString), typeof (IDuplexString));
        }

        private void RegisterType(Type serialize, Type deserialize)
        {
            _serializers.Add(serialize, new Serializer<CompactBinaryWriter<OutputBuffer>>(serialize));
            _deserializers.Add(deserialize, new Deserializer<CompactBinaryReader<InputBuffer>>(serialize));
        }

        public byte[] Serialize(IBaseObject obj)
        {
            var output = new OutputBuffer();
            var writer = new CompactBinaryWriter<OutputBuffer>(output);

            _serializers[obj.GetType()].Serialize(obj, writer);

            return output.Data.Array;
        }

        public IBaseObject Deserialize(Type t, byte[] obj)
        {
            var input = new InputBuffer(obj);
            var reader = new CompactBinaryReader<InputBuffer>(input);

            return (IBaseObject)_deserializers[t].Deserialize(reader);
        }

        public BondObjectFactory GetFactory()
        {
            return new BondObjectFactory();
        }
    }
}

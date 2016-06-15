using cerealize.common.Objects.Contracts.Strings;
using ProtoBuf;

namespace cerealize.serializers.protobuf.Objects.Strings
{
    [ProtoContract]
    public class ProtobufSimpleString : ISimpleString
    {
        [ProtoMember(1)]
        public string FirstName { get; set; }
    }
}
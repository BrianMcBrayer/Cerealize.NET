using cerealize.common.Objects.Contracts.Strings;
using ProtoBuf;

namespace cerealize.serializers.protobuf.Objects.Strings
{
    [ProtoContract]
    public class ProtobufDuplexString : IDuplexString
    {
        [ProtoMember(1)]
        public string FirstName { get; set; }
        [ProtoMember(2)]
        public string LastName { get; set; }
    }
}
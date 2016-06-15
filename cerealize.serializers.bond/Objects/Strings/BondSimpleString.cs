using Bond;
using cerealize.common.Objects.Contracts.Strings;

namespace cerealize.serializers.bond.Objects.Strings
{
    [Schema]
    public class BondSimpleString : ISimpleString
    {
        [Id(0)]
        public string FirstName { get; set; }
    }
}
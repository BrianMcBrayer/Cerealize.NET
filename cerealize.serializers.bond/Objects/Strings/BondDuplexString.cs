using Bond;
using cerealize.common.Objects.Contracts.Strings;

namespace cerealize.serializers.bond.Objects.Strings
{
    [Schema]
    public class BondDuplexString : IDuplexString
    {
        [Id(0)]
        public string FirstName { get; set; }
        [Id(1)]
        public string LastName { get; set; }
    }
}
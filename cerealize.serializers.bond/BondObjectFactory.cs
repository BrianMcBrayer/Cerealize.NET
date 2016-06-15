using cerealize.common.Objects.Contracts.Strings;
using cerealize.common.Serializer;
using cerealize.serializers.bond.Objects.Strings;

namespace cerealize.serializers.bond
{
    public class BondObjectFactory : BaseObjectFactory
    {
        public BondObjectFactory()
        {
            RegisterFactoryType(typeof(ISimpleString), CreateSimpleString);
            RegisterFactoryType(typeof (IDuplexString), CreateBondDuplexString);
        }

        private static BondSimpleString CreateSimpleString()
        {
            return new BondSimpleString
            {
                FirstName = "Bentworthy"
            };
        }

        private static BondDuplexString CreateBondDuplexString()
        {
            return new BondDuplexString
            {
                FirstName = "Bentworty",
                LastName = "Smith"
            };
        }
    }
}
using System;
using cerealize.common.Objects.Contracts;

namespace cerealize.common.Serializer.Contracts
{
    public interface ISerializer
    {
        
    }

    public interface ISerializer<out TFactory> : ISerializer
        where TFactory : IObjectFactory
    {
        byte[] Serialize(IBaseObject obj);

        IBaseObject Deserialize(Type t, byte[] obj);

        TFactory GetFactory();
    }
}
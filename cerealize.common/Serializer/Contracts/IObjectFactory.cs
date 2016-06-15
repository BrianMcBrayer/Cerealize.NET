using System;
using cerealize.common.Objects.Contracts;
using cerealize.common.Objects.Contracts.Strings;

namespace cerealize.common.Serializer.Contracts
{
    public interface IObjectFactory
    {
        bool CanCreateObject(Type t);

        object CreateObject(Type t);
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using cerealize.common.Objects.Contracts;
using cerealize.common.Objects.Contracts.Strings;
using cerealize.common.Serializer.Contracts;
using cerealize.serializers.protobuf;

namespace cerealize
{
    class Program
    {
        private static readonly List<int> _series = new List<int>
        {
            1000,
            10000,
            100000
        };

        private static readonly List<Type> _testTypes = new List<Type>
        {
            typeof (IDuplexString),
            typeof (ISimpleString)
        };

        private static readonly List<ISerializer<IObjectFactory>> _serializers = new List<ISerializer<IObjectFactory>>
        {
            new ProtobufSerializer()
        };

        static void Main(string[] args)
        {
            foreach (var series in _series)
            {
                foreach (var testType in _testTypes)
                {
                    foreach (var serializer in _serializers)
                    {
                        var serializerFactory = serializer.GetFactory();

                        var simpleString = serializerFactory.CreateObject(testType);
                        // Prime the serializer by serializing an object first
                        var serializedObject = serializer.Serialize((IBaseObject)simpleString);
                        var stopwatch = new Stopwatch();
                        stopwatch.Restart();

                        for (int i = 0; i < series; i++)
                        {
                            serializedObject = serializer.Serialize((IBaseObject)simpleString);
                        }

                        stopwatch.Stop();

                        Console.WriteLine("{1}\tserialized\t{2} times\t{0}ms\t{3}", stopwatch.ElapsedMilliseconds, testType.Name, series, serializer.GetType().Name);

                        var deserializedObject = serializer.Deserialize(testType, serializedObject);
                        stopwatch.Restart();

                        for (int i = 0; i < series; i++)
                        {
                            deserializedObject = serializer.Deserialize(testType, serializedObject);
                        }

                        stopwatch.Stop();

                        Console.WriteLine("{1}\tdeserialized\t{2} times\t{0}ms\t{3}", stopwatch.ElapsedMilliseconds, testType.Name, series, serializer.GetType().Name);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}

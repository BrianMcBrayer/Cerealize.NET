using System;
using System.Collections.Generic;
using System.Diagnostics;
using cerealize.common;
using cerealize.common.Objects.Contracts;
using cerealize.common.Objects.Contracts.Strings;
using cerealize.common.Serializer.Contracts;
using cerealize.serializers.bond;
using cerealize.serializers.protobuf;

namespace cerealize
{
    class Program
    {
        private static readonly List<Metric> Metrics = new List<Metric>();

        private static readonly List<int> Series = new List<int>
        {
            1000,
            10000,
            100000
        };

        private static readonly List<Type> ObjectTypes = new List<Type>
        {
            typeof (IDuplexString),
            typeof (ISimpleString)
        };

        private static readonly List<ISerializer<IObjectFactory>> Serializers = new List<ISerializer<IObjectFactory>>
        {
            new ProtobufSerializer(),
            new BondSerializer()
        };

        static void Main(string[] args)
        {
            foreach (var series in Series)
            {
                foreach (var objectType in ObjectTypes)
                {
                    foreach (var serializer in Serializers)
                    {
                        var serializerFactory = serializer.GetFactory();

                        if (!serializerFactory.CanCreateObject(objectType))
                        {
                            Metrics.Add(new Metric
                            {
                                SeriesId = series.ToString(),
                                ObjectId = objectType.Name,
                                SerializerId = serializer.GetType().Name,
                                PassedAllTests = false,
                                FailureMessages = new List<string>
                                {
                                    "Serialize's factory cannot create this object type"
                                }
                            });
                            continue;
                        }

                        var objectToSerialize = serializerFactory.CreateObject(objectType);
                        // TODO Check objectToSerialize object against tests
                        // Prime the serializer by serializing an object first
                        var serializedObject = serializer.Serialize((IBaseObject)objectToSerialize);
                        var stopwatch = new Stopwatch();
                        stopwatch.Restart();

                        for (int i = 0; i < series; i++)
                        {
                            serializedObject = serializer.Serialize((IBaseObject)objectToSerialize);
                        }

                        stopwatch.Stop();
                        Metrics.Add(new Metric
                        {
                            SeriesId = series.ToString(),
                            ObjectId = objectType.Name,
                            SerializerId = serializer.GetType().Name,
                            ElapsedTimeForSamplingSize = stopwatch.Elapsed,
                            SamplingSize = series,
                            SerializedBytesLength = serializedObject.Length,
                            Stage = MetricStage.Serialized
                        });

                        Console.WriteLine("{1}\tserialized\t{2} times\t{0}ms\t{3}", stopwatch.ElapsedMilliseconds, objectType.Name, series, serializer.GetType().Name);

                        var deserializedObject = serializer.Deserialize(objectType, serializedObject);
                        //TODO Check deserializedObject against tests
                        stopwatch.Restart();

                        for (int i = 0; i < series; i++)
                        {
                            deserializedObject = serializer.Deserialize(objectType, serializedObject);
                        }

                        stopwatch.Stop();
                        Metrics.Add(new Metric
                        {
                            SeriesId = series.ToString(),
                            ObjectId = objectType.Name,
                            SerializerId = serializer.GetType().Name,
                            ElapsedTimeForSamplingSize = stopwatch.Elapsed,
                            SamplingSize = series,
                            Stage = MetricStage.Deserialized
                        });

                        Console.WriteLine("{1}\tdeserialized\t{2} times\t{0}ms\t{3}", stopwatch.ElapsedMilliseconds, objectType.Name, series, serializer.GetType().Name);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}

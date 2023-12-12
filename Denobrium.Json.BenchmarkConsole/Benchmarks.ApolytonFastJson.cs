﻿using Apolyton.FastJson.Data;
using consoletest.DataObjects;
using System;
using System.Diagnostics;

namespace consoletest
{
    public static partial class Benchmarks
    {
        private class ApolytonFastJsonBenchmarks_Old
        {
            /// <summary>
            /// Deserialize from a string with type information.
            /// </summary>
            internal static void Deserialize()
            {
                Console.WriteLine();
                Console.Write("(A4) JSON readobject                  ");
                BenchmarkDataClass c = CreateTestedObject();

                // Type information needs to be in the string, otherwise readobject doesn't work.
                Apolyton.FastJson.Json.Current.DefaultParameters.UseTypeExtension = true;
                string jsonText = Apolyton.FastJson.Json.Current.ToJson(c); 
                Apolyton.FastJson.Json.Current.DefaultParameters.UseTypeExtension = false;

                InitTestRun();

                for (int pp = 0; pp < numberOfRuns; pp++)
                {
                    stopwatch.Reset();
                    stopwatch.Start();

                    BenchmarkDataClass deserializedStore;

                    for (int i = 0; i < iterationsPerRun; i++)
                    {
                        deserializedStore = (BenchmarkDataClass)Apolyton.FastJson.Json.Current.ReadObject(jsonText);
                    }

                    stopwatch.Stop();
                    Console.Write("\t" + stopwatch.ElapsedMilliseconds);
                    testRunDurations.Add(stopwatch.ElapsedMilliseconds);
                }

                WriteAverage(true);
            }

            /// <summary>
            /// Deserialize from a string with type information.
            /// </summary>
            internal static void DeserializeByType()
            {
                Console.WriteLine();
                Console.Write("(A5) JSON readobject with given type ");
                BenchmarkDataClass c = CreateTestedObject();

                // No type information needs to be in the string, since we will provide the root level element later.s
                string jsonText = Apolyton.FastJson.Json.Current.ToJson(c); 
                InitTestRun();

                for (int pp = 0; pp < numberOfRuns; pp++)
                {
                    stopwatch.Reset();
                    stopwatch.Start();

                    BenchmarkDataClass deserializedStore;

                    for (int i = 0; i < iterationsPerRun; i++)
                    {
                        deserializedStore = (BenchmarkDataClass)Apolyton.FastJson.Json.Current.ReadObject(jsonText, typeof(BenchmarkDataClass));
                    }

                    stopwatch.Stop();
                    Console.Write("\t" + stopwatch.ElapsedMilliseconds);
                    testRunDurations.Add(stopwatch.ElapsedMilliseconds);
                }

                WriteAverage(true);
            }

            /// <summary>
            /// Deserialize into a json object.
            /// </summary>
            internal static void Apolyton_FastJson_Deserialize_JsonObject()
            {
                Console.WriteLine();
                Console.Write("(A3) JSON decode JsonValue         ");
                BenchmarkDataClass c = CreateTestedObject();

                Apolyton.FastJson.Json.Current.DefaultParameters.UseTypeExtension = false;
                string jsonText = Apolyton.FastJson.Json.Current.ToJson(c);

                InitTestRun();

                for (int pp = 0; pp < numberOfRuns; pp++)
                {
                    stopwatch.Reset();
                    stopwatch.Start();

                    JsonObject deserializedStore;

                    for (int i = 0; i < iterationsPerRun; i++)
                    {
                        deserializedStore = (JsonObject)Apolyton.FastJson.Json.Current.ReadJsonValue(jsonText);
                    }

                    stopwatch.Stop();
                    Console.Write("\t" + stopwatch.ElapsedMilliseconds);
                    testRunDurations.Add(stopwatch.ElapsedMilliseconds);
                }

                WriteAverage(true);
            }

            /// <summary>
            /// Deserialize into a json object and build up a given instance with its values.
            /// </summary>
            internal static void Deserialize_JsonObject_BuildUp_NoTypeExtension()
            {
                Console.WriteLine();
                Console.Write("(A2) JSON decode JVal+Bld           ");
                BenchmarkDataClass c = CreateTestedObject();
                BenchmarkDataClass target;

                Apolyton.FastJson.Json.Current.DefaultParameters.UseTypeExtension = false;
                string jsonText = Apolyton.FastJson.Json.Current.ToJson(c);

                InitTestRun();

                for (int pp = 0; pp < numberOfRuns; pp++)
                {
                    stopwatch.Reset();
                    stopwatch.Start();

                    JsonObject deserializedStore;
                    target = new BenchmarkDataClass();

                    for (int i = 0; i < iterationsPerRun; i++)
                    {
                        deserializedStore = (JsonObject)Apolyton.FastJson.Json.Current.ReadJsonValue(jsonText);
                        Apolyton.FastJson.Json.Current.BuildUp(target, deserializedStore);
                    }

                    stopwatch.Stop();
                    Console.Write("\t" + stopwatch.ElapsedMilliseconds);
                    testRunDurations.Add(stopwatch.ElapsedMilliseconds);
                }

                WriteAverage(true);
            }


            /// <summary>
            /// Deserialize into a json value.
            /// </summary>
            internal static void Deserialize_JsonValue()
            {
                Console.WriteLine();
                Console.Write("(A1) JSON decode JVal                 ");
                BenchmarkDataClass c = CreateTestedObject();
                BenchmarkDataClass target;

                Apolyton.FastJson.Json.Current.DefaultParameters.UseTypeExtension = false;
                string jsonText = Apolyton.FastJson.Json.Current.ToJson(c);

                InitTestRun();

                for (int pp = 0; pp < numberOfRuns; pp++)
                {
                    stopwatch.Reset();
                    stopwatch.Start();

                    JsonObject deserializedStore;
                    target = new BenchmarkDataClass();

                    for (int i = 0; i < iterationsPerRun; i++)
                    {
                        deserializedStore = (JsonObject)Apolyton.FastJson.Json.Current.ReadJsonValue(jsonText);
                    }

                    stopwatch.Stop();
                    Console.Write("\t" + stopwatch.ElapsedMilliseconds);
                    testRunDurations.Add(stopwatch.ElapsedMilliseconds);
                }

                WriteAverage(true);
            }

            /// <summary>
            /// Deserialize into a json object, build it up into a given instance with type extension support 
            /// and data contract type descriptor.
            /// </summary>
            internal static void Deserialize_JsonObject_BuildUp_DataContractTypeExtension()
            {
                Console.WriteLine();
                Console.Write("(A3) JSON decode JVal+Bld+TExt+DC");
                BenchmarkDataClass c = CreateTestedObject();
                BenchmarkDataClass target;

                Apolyton.FastJson.Json.Current.DefaultParameters.UseTypeExtension = true;
                Apolyton.FastJson.Json.Current.DefaultParameters.RegisterTypeDescriptor(
                    new Apolyton.FastJson.Registry.DataContractTypeDescriptor(typeof(Benchmarks).Assembly));
                string jsonText = Apolyton.FastJson.Json.Current.ToJson(c);

                InitTestRun();

                for (int pp = 0; pp < numberOfRuns; pp++)
                {
                    stopwatch.Reset();
                    stopwatch.Start();

                    JsonObject deserializedStore;
                    target = new BenchmarkDataClass();

                    for (int i = 0; i < iterationsPerRun; i++)
                    {
                        deserializedStore = (JsonObject)Apolyton.FastJson.Json.Current.ReadJsonValue(jsonText);
                        Apolyton.FastJson.Json.Current.BuildUp(target, deserializedStore);
                    }

                    stopwatch.Stop();
                    Console.Write("\t" + stopwatch.ElapsedMilliseconds);
                    testRunDurations.Add(stopwatch.ElapsedMilliseconds);
                }

                WriteAverage(true);
            }

            /// <summary>
            /// Serialize into a json string.
            /// </summary>
            internal static void Serialize()
            {
                Console.WriteLine();
                Console.Write("(A) FastJSON encode      ");
                BenchmarkDataClass c = CreateTestedObject();

                InitTestRun();

                for (int pp = 0; pp < numberOfRuns; pp++)
                {
                    stopwatch.Reset();
                    stopwatch.Start();
                    string jsonText = null;

                    for (int i = 0; i < iterationsPerRun; i++)
                    {
                        jsonText = Apolyton.FastJson.Json.Current.ToJson(c);
                    }

                    stopwatch.Stop();
                    Console.Write("\t" + stopwatch.ElapsedMilliseconds);
                    testRunDurations.Add(stopwatch.ElapsedMilliseconds);
                }

                WriteAverage(true);
            }
        }
    }
}
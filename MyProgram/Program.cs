using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Reflection;
using GeneratorOutput;

namespace MyProgram
{
    [MemoryDiagnoser]
    public class Program
    {
        private Test _object = Test.Create();
        static void Main(string[] args)
        {
            //BenchmarkRunner.Run<Program>();
            new Program().UseJsonSerializer();
        }

        //Original serializer
        [Benchmark]
        public void UseJsonSerializer() => UseSerializer(new JsonSerializer());

        //Serializer using LINQ
        [Benchmark]
        public void UseJsonSerializerReworked() => UseSerializer(new JsonSerializerReworked());

        //Original generated serializer
        [Benchmark]
        public void UseGeneratedSerializer() => UseSerializer(new GeneratedSerializer());

        public void UseSerializer(dynamic Serializer)
        {
            Console.WriteLine($"{Serializer.GetType().Name}");
            Console.WriteLine("\t" + Serializer.Serialize(_object));
        }
    }
}

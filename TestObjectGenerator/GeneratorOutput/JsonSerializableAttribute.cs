using System;
namespace GeneratorOutput
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JsonSerializableAttribute : Attribute
    {
        public JsonSerializableAttribute() { }
    }
}
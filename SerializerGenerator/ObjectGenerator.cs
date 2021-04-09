using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SerializerGenerator
{
    public class ObjectGenerator : ISourceGenerator
    {
        private readonly Dictionary<char, char> _letters = new Dictionary<char, char> {
            { '0', 'a' },
            { '1', 'b' },
            { '2', 'c' },
            { '3', 'd' },
            { '4', 'e' },
            { '5', 'f' },
            { '6', 'g' },
            { '7', 'h' },
            { '8', 'i' },
            { '9', 'j' }
        };

        public void Execute(GeneratorExecutionContext context)
        {
            var properties = ZeroTo(20).ToList();

            //Create class
            var sb = new StringBuilder();
            sb.Append("namespace MyProgram{ [JsonSerializable] public class Test {");

            //Add properies
            properties.ForEach(p => sb.Append($"public string {p} {{ get; set; }}"));

            //Create constructor
            sb.Append("public Test(){");
            sb.Append(string.Join("; ", properties.Select(p => $"{p} = \"p\"")));
            sb.Append(";}}}");

            context.AddSource("Test", SourceText.From(sb.ToString(), Encoding.UTF8));
        }

        private IEnumerable<string> ZeroTo(int c)
        {
            for (var i = 0; i < c; i++)
                yield return ReplaceNumbers(i);
        }

        private string ReplaceNumbers(int i) => string.Concat(i.ToString().Select(c => _letters[c]));

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
        }
    }
}
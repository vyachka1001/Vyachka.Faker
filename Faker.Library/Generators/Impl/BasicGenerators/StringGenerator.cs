using Faker.Library.Generators.Entity;
using System;
using System.Text;

namespace Faker.Library.Generators.Impl.BasicGenerators
{
    public class StringGenerator : IGenerator
    {
        public object Generate(GeneratorContext context)
        {
            var size = context.Random.Next(1, 20);
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < size; i++)
                stringBuilder.Append((char)context.Random.Next());

            return stringBuilder.ToString();
        }

        public bool CanGenerate(Type t)
        {
            return typeof(string) == t;
        }
    }
}
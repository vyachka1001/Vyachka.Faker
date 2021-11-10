using Faker.Library.Generators.Entity;
using System;

namespace Faker.Library.Generators.Impl.BasicGenerators
{
    public class LongGenerator : IGenerator
    {
        public object Generate(GeneratorContext context)
        {
            long result = context.Random.Next(int.MinValue >> 32, int.MaxValue >> 32);

            result = (result << 32);
            result |= context.Random.Next(int.MinValue, int.MaxValue);

            return result;
        }

        public bool CanGenerate(Type t)
        {
            return typeof(long) == t;
        }
    }
}
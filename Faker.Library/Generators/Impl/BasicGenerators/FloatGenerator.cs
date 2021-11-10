using System;
using Faker.Library.Generators.Entity;

namespace Faker.Library.Generators.Impl.BasicGenerators
{
    public class FloatGenerator : IGenerator
    {
        public object Generate(GeneratorContext context)
        {
            return (float)context.Random.NextDouble();
        }

        public bool CanGenerate(Type t)
        {
            return typeof(float) == t;
        }
    }
}
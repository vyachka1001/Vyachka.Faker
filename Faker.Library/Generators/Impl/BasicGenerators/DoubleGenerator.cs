using Faker.Library.Generators.Entity;
using System;

namespace Faker.Library.Generators.Impl.BasicGenerators
{
    public class DoubleGenerator : IGenerator
    {
        public object Generate(GeneratorContext context)
        {
            return context.Random.NextDouble();
        }

        public bool CanGenerate(Type t)
        {
            return typeof(double) == t;
        }
    }
}
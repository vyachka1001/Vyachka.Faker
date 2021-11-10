using Faker.Library.Generators.Entity;
using System;

namespace Faker.Library.Generators.Impl.BasicGenerators
{
    public class BoolGenerator : IGenerator
    {
        public object Generate(GeneratorContext context)
        {
            return Convert.ToBoolean(context.Random.Next(0, 1));
        }

        public bool CanGenerate(Type t)
        {
            return typeof(bool) == t;
        }
    }
}
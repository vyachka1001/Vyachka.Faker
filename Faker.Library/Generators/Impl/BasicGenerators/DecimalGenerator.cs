using Faker.Library.Generators.Entity;
using System;

namespace Faker.Library.Generators.Impl.BasicGenerators
{
    public class DecimalGenerator : IGenerator
    {
        public object Generate(GeneratorContext context)
        {
            byte scale = (byte)context.Random.Next(29);
            bool sign = context.Random.Next(2) == 1;

            return new decimal(context.Random.Next(), context.Random.Next(), context.Random.Next(), sign, scale);
        }

        public bool CanGenerate(Type t)
        {
            return typeof(decimal) == t;
        }
    }
}
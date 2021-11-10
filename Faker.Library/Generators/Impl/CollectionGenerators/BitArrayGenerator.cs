using System;
using System.Collections;
using Faker.Library.Generators.Entity;

namespace Faker.Library.Generators.Impl.CollectionGenerators
{
    public class BitArrayGenerator : IGenerator
    {
        public object Generate(GeneratorContext context)
        {
            var size = context.Random.Next(1, 20);
            var array = new bool[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = Convert.ToBoolean(context.Random.Next(0, 1));
            }

            return new BitArray(array);
        }

        public bool CanGenerate(Type t)
        {
            return typeof(BitArray) == t;
        }
    }
}
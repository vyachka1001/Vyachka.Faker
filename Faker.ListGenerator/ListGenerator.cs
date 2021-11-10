using System;
using System.Collections;
using System.Collections.Generic;
using Faker.Library.Generators.Entity;

namespace Faker.Library.Generators.Impl.CollectionGenerators
{
    public class ListGenerator : IGenerator
    {
        public object Generate(GeneratorContext context)
        {
            var size = context.Random.Next(1, 20);
            var list = (IList)Activator.CreateInstance(context.TargetType);

            for (int i = 0; i < size; i++)
            {
                list.Add(context.Faker.Create(context.TargetType.GetGenericArguments()[0]));
            }

            return list;
        }

        public bool CanGenerate(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}
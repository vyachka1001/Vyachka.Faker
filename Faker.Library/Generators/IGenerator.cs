using System;

namespace Faker.Library.Generators
{
    public interface IGenerator
    {
        public object Generate(Entity.GeneratorContext context);

        public bool CanGenerate(Type t);
    }
}

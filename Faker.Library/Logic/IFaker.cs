using System;

namespace Faker.Library.Logic
{
    public interface IFaker
    {
        T Create<T>();
        object Create(Type t);
    }
}

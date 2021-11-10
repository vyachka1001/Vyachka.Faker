using System;

namespace Faker.Library.Exceptions
{
    public class CyclicDependencyException : Exception
    {
        public CyclicDependencyException(string message) : base(message)
        {

        }
    }
}

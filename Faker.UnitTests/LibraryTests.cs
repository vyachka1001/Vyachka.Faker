using Faker.Library.Logic;
using Faker.Library.Logic.Impl;
using Faker.Library.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Faker.UnitTests
{
    [TestFixture]
    class LibraryTests
    {
        IFaker faker;

        [SetUp]
        public void Setup()
        {
            Assembly.LoadFile(@"D:\STUDYING\3_course\ÑÏÏ\Labs\Vyachka.Faker\Faker.IntGenerator\bin\Debug\net5.0\ref\Faker.IntGenerator.dll");
            Assembly.LoadFile(@"D:\STUDYING\3_course\ÑÏÏ\Labs\Vyachka.Faker\Faker.ListGenerator\bin\Debug\net5.0\ref\Faker.ListGenerator.dll");
            faker = new FakerImpl();
        }

        [TestCase("")]
        [TestCase(0)]
        [TestCase(0.0)]
        [TestCase(true)]
        [TestCase(0L)]
        public void FakerImpl_CreateBasicTypes_NoExceptions<T>(T par)
        {
            Assert.DoesNotThrow(() => faker.Create<T>());
        }

        [Test]
        public void FakerImpl_CreateStruct_RandomizerWorks()
        {
            DateTime defObj = new DateTime();
            DateTime fakerObj = faker.Create<DateTime>();
            Assert.IsFalse(defObj == fakerObj);
        }

        [Test]
        public void FakerImpl_CreateList_RandomizerWorks()
        {
            List<int> defObj = new List<int>();
            List<int> fakerObj = faker.Create<List<int>>();
            Assert.IsFalse(defObj == fakerObj);
        }

        [TestCase(' ')]
        [TestCase(0UL)]
        public void FakerImpl_NoSuchGenerator_ArgumentExceptionThrows<T>(T par)
        {
            Assert.Throws<ArgumentException>(() => faker.Create<T>());
        }

        public class TestClass
        {
            public int a;
            public int b;
        }

        [Test]
        public void FakerImpl_CreateClass_RandomizerWorks()
        {
            var testClass = faker.Create<TestClass>();
            Assert.IsTrue(testClass.a != 0 && testClass.b != 0);
        }

        public class A
        {
            public B B { get; set; }
        }

        public class B
        {
            public C C { get; set; }
        }

        public class C
        {
            public A A { get; set; }
        }

        [Test]
        public void FakerImpl_CyclicDependencyDetected_CyclicDependencyExceptionThrows()
        {
            Assert.Throws<CyclicDependencyException>(() => faker.Create<A>());
        }
    }
}

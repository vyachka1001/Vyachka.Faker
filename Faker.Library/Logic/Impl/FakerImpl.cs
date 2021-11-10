using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Faker.Library.Comparators;
using Faker.Library.Exceptions;
using Faker.Library.Generators;
using Faker.Library.Generators.Entity;

namespace Faker.Library.Logic.Impl
{
    public class FakerImpl : IFaker
    {
        private List<IGenerator> _generators;
        private List<Type> _createdTypesHolder;
        private Random _randomizer;

        public FakerImpl()
        {
            _randomizer = new Random();
            _createdTypesHolder = new List<Type>();
            _generators = new List<IGenerator>();

            LoadGenerators();
        }

        private void LoadGenerators()
        {
            var generatorType = typeof(IGenerator);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in assemblies)
            {
                foreach (var t in a.GetTypes())
                {
                    if (t.IsClass && t.GetInterfaces().Contains(generatorType))
                    {
                        _generators.Add((IGenerator)Activator.CreateInstance(t));
                    }
                }
            }
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        public object Create(Type type)
        {
            foreach (var g in _generators)
            {
                if (g.CanGenerate(type)) return g.Generate(new GeneratorContext(_randomizer, type, this));
            }

            if (_createdTypesHolder.Contains(type))
            {
                throw new CyclicDependencyException("Cyclic dependency was detected.");
            }

            _createdTypesHolder.Add(type);

            object obj = Init(type);
            InitProperties(obj);
            InitFields(obj);

            _createdTypesHolder.Remove(type);

            return obj;
        }

        private object Init(Type t)
        {
            var constructors = GetSortedConstructorInfos(t, new ConstructorArgumentAmountComparerDesc());

            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = new List<object>();
                    foreach (var p in constructor.GetParameters())
                    {
                        parameters.Add(Create(p.ParameterType));
                    }

                    return constructor.Invoke(parameters.ToArray());
                }
                catch (CyclicDependencyException)
                {
                    throw;
                }
                catch
                {
                    //Ignored
                }
            }

            throw new ArgumentException($"Cannot create object of type {t}");
        }


        private void InitFields(object obj)
        {
            Type t = obj.GetType();
            var fields = t.GetFields();

            foreach (var field in fields)
            {
                try
                {
                    if (Equals(field.GetValue(obj), GetDefaultValue(field.FieldType)))
                    {
                        field.SetValue(obj, Create(field.FieldType));
                    }
                }
                catch (CyclicDependencyException)
                {
                    throw;
                }
                catch
                {
                    //Ignored
                }
            }

        }

        private void InitProperties(object obj)
        {
            Type t = obj.GetType();
            var fields = t.GetProperties();

            foreach (var prop in fields)
            {
                try
                {
                    if (Equals(prop.GetValue(obj), GetDefaultValue(prop.PropertyType)))
                    {
                        prop.SetValue(obj, Create(prop.PropertyType));
                    }
                }
                catch (CyclicDependencyException)
                {
                    throw;
                }
                catch
                {
                    // Ignored
                }
            }
        }

        private IEnumerable<ConstructorInfo> GetSortedConstructorInfos(Type type, IComparer<ConstructorInfo> constrCompare)
        {
            var constructorsInfoList = type.GetConstructors().ToList();
            constructorsInfoList.Sort(constrCompare);

            return constructorsInfoList;
        }

        private static object GetDefaultValue(Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }
    }
}
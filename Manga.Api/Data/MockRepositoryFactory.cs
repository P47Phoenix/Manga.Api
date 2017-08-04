using System;
using System.Collections.Generic;
using Manga.Api.Models;

namespace Manga.Api.Controllers
{
    public class MockRepositoryFactory : IRepositoryFactory
    {
        private class GenericKey<T, T1> where T1 : class
        { }

        private readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();


        public IRepository<T, T1> CreateRepository<T, T1>() where T1 : class
        {
            var key = typeof(GenericKey<T, T1>);

            if (_dictionary.ContainsKey(key)) return (IRepository<T, T1>) _dictionary[key];

            if (typeof(Chapter) == typeof(T1) && typeof(string) == typeof(T))
            {
                _dictionary.Add(key, BuildSimulatorModel.GetMangaChapter());
            }
            else if (typeof(Series) == typeof(T1) && typeof(string) == typeof(T))
            {
                _dictionary.Add(key, BuildSimulatorModel.GetSeriesChapter());
            }
            else
            {
                throw new InvalidOperationException($"Factory does not support the requested repository of {typeof(IRepository<T, T1>).Name}");
            }

            return (IRepository<T, T1>)_dictionary[key];
        }
    }
}
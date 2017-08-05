using System;
using System.Collections.Generic;
using Manga.Api.Models;

namespace Manga.Api.Controllers
{
    public class MockRepositoryFactory : IRepositoryFactory
    {
        private class GenericKey<TId, TRefId, TRecord> where TRecord : class
        { }

        private readonly Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();


        public IRepository<TId, TRefId, TRecord> CreateRepository<TId, TRefId, TRecord>() where TRecord : class
        {
            var key = typeof(GenericKey<TId, TRefId, TRecord>);

            if (_dictionary.ContainsKey(key)) return (IRepository<TId, TRefId, TRecord>)_dictionary[key];

            switch (key)
            {
                case Type t when t == typeof(GenericKey<string, string, Chapter>):
                    _dictionary.Add(key, BuildSimulatorModel.GetMangaChapter());
                    break;
                case Type t when t == typeof(GenericKey<string, string, Series>):
                    _dictionary.Add(key, BuildSimulatorModel.GetSeriesChapter());
                    break;
                case Type t when t == typeof(GenericKey<string, string, Page>):
                    _dictionary.Add(key, BuildSimulatorModel.GetPageChapter());
                    break;
                default:
                    throw new InvalidOperationException($"Factory does not support the requested repository of {typeof(IRepository<TId, TRefId, TRecord>).Name}");
            }

            return (IRepository<TId, TRefId, TRecord>)_dictionary[key];
        }
    }
}
using Contacts.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Contacts.Services.Repository
{
    

    public class Repository : IRepository
    {
        private Lazy<SQLiteAsyncConnection> _dataBase;

        public Repository()
        {
            _dataBase = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "profilebook.db");
                var database = new SQLiteAsyncConnection(path);
                database.CreateTableAsync<ProfileModel>();
                return database;
            });

        }
        public Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _dataBase.Value.DeleteAsync(entity);
        }

        public Task<List<T>> GetAllAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _dataBase.Value.Table<T>().ToListAsync();
        }

        Task<List<T>> IRepository.GetAllAsync<T>()
        {
            return _dataBase.Value.Table<T>().ToListAsync();
        }

        public Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _dataBase.Value.InsertAsync(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _dataBase.Value.UpdateAsync(entity);
        }

        
    }
}

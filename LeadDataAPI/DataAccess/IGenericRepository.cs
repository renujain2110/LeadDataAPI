using LeadDataAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LeadDataAPI.DataAccess
{
    /// <summary>
    /// Generic template for CRUD operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Return all T objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Return a single T object that matches given id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>T object</returns>
        T GetById(long id);

        /// <summary>
        /// Performs lookup based on given condition
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns>Returns all T objects that match the lookup condition</returns>
        IEnumerable<T> Search(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Inserts new T object
        /// </summary>
        /// <param name="obj">T object</param>
        void Insert(T obj);

        /// <summary>
        /// Updates T object
        /// </summary>
        /// <param name="obj">T object</param>
        void Update(T obj);

        /// <summary>
        /// Deletes T object
        /// </summary>
        /// <param name="obj">T object</param>
        void Delete(long id);

    }
}
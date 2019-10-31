using LeadDataAPI.Helpers;
using LeadDataAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LeadDataAPI.DataAccess
{
    /// <summary>
    /// Repository for CRUD operations
    /// </summary>
    public class LeadRepository : IGenericRepository<Lead>
    {
        private DataContext _context;
        public LeadRepository(DataContext context)
        {
            this._context = context;
        }


        /// <summary>
        /// Return all Lead objects
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Lead> GetAll()
        {
            return _context.Leads;
        }


        /// <summary>
        /// Return a single Lead object that matches given id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Lead object</returns>
        public Lead GetById(long id)
        {
            return _context.Leads.Find(id);
        }


        /// <summary>
        /// Performs lookup based on given condition
        /// </summary>
        /// <param name="predicate">Condition</param>
        /// <returns>Returns all Lead objects that match the lookup condition</returns>
        public IEnumerable<Lead> Search(Expression<Func<Lead, bool>> predicate)
        {
            return _context.Leads.Where(predicate);
        }


        /// <summary>
        /// Inserts new Lead object
        /// </summary>
        /// <param name="obj">Lead object</param>
        public void Insert(Lead obj)
        {
            _context.Leads.Add(obj);
            _context.SaveChanges();
        }


        /// <summary>
        /// Updates Lead object
        /// </summary>
        /// <param name="obj">Lead object</param>
        public void Update(Lead leadParam)
        {
            var lead = _context.Leads.Find(leadParam.LeadId);

            lead.FirstName = leadParam.FirstName;
            lead.LastName = leadParam.LastName;
            lead.AcceptTerms = leadParam.AcceptTerms;
            lead.Email = leadParam.Email;
            lead.PostCode = leadParam.PostCode;
            lead.Company = leadParam.Company;

            _context.Leads.Update(lead);
            _context.SaveChanges();
        }


        /// <summary>
        /// Deletes Lead object
        /// </summary>
        /// <param name="obj">Lead object</param>
        public void Delete(long leadId)
        {
            var lead = _context.Leads.Find(leadId);

            if (lead != null)
            {
                _context.Leads.Remove(lead);
                _context.SaveChanges();
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace LeadDataAPI.Entities
{
    /// <summary>
    /// Lead Entity class
    /// </summary>
    public class Lead
    {
        public long LeadId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string PostCode { get; set; }
        public bool AcceptTerms { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

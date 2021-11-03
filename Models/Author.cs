using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditec.Models
{
    public class Author : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public Author(string firstName, string lastName)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //}
    }
}

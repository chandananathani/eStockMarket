using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Model.Common
{
    /// <summary>
    /// class for declaring user details
    /// </summary>
    public class User
    {
        /// <summary>
        /// specifies user id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// specifies first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// specifies last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// specifies user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// specifies email id
        /// </summary>
        [BsonId]
        public string Email { get; set; }

        /// <summary>
        /// specifies password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// specifies is Active
        /// </summary>
        public int IsActive { get; set; }

        /// <summary>
        /// specifies Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}

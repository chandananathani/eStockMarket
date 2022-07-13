using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Model.Common
{
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [BsonId]
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

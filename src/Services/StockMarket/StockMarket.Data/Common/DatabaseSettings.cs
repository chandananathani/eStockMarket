using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMarket.Data.Common
{
    /// <summary>
    /// service class for database settings
    /// </summary>
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CompanyCollectionName { get; set; }
        public string StockCollectionName { get; set; }
        public string UserCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    /// <summary>
    /// interface class for database settings
    /// </summary>
    public interface IDatabaseSettings
    {
        string CompanyCollectionName { get; set; }
        string StockCollectionName { get; set; }
        string UserCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
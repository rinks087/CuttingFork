using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSFTWebAPI.Services
{
    public class SQLQueryProvider
    {
        private readonly IConfiguration _configuration;

        public SQLQueryProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetQuery(string queryName)
        {
            return _configuration.GetValue<string>($"StoredProcedures:{queryName}");
        }
    }

}

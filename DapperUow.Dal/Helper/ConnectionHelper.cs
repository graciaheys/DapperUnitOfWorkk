using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DapperUow.Dal.Helper
{
    public static class ConnectionHelper
    {
        // connection string'mizi getirmesi için kullanacağız.  Console üzerinde bir app.config oluşturup ordan okumayı planlıyoruz.
        public static string GetConnectionString => ConfigurationManager.ConnectionStrings["DapperUow"].ConnectionString;
    }
}

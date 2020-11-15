using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DapperUow.Dal.Repositories
{
    internal abstract class RepositoryBase
    {
        // her yerden ulaşılsın istemiyorum private set o yüzden.  Ctrl + . basıyorum referansı ekliyorum.
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }

        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        // Repositoryler için base class'ımı oluşturdum. Şimdi entity için oluşturacağız önce interfaceler
    }
}

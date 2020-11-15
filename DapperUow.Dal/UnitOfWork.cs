using DapperUow.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DapperUow.Dal
{
    // ctrl + . ile implement ediyoruz.
    // interface 'ini implement ettik.
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IProductRepository _productRepository;
        private bool _dispose;

        // ctor + tab tab
        public UnitOfWork(string connectionString)
        {
            // SqlConnection için ctrl + . yapıp gerekli kütüphaneyi indirmeniz gerekiyor. =>  using System.Data.SqlClient;
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();

        }

        public IProductRepository ProductRepository 
        { 
            get { return _productRepository ?? (_productRepository = new ProductRepository(_transaction));  }
        }

        public void Commit()
        {
            try
            {
                // burda hata yaptık commit yapması gerekirken sürekli hataya düşecek burası yorgunluk böyle birşey :)))  hiçbir türlü commit olmaz düzeltiyoruz :)))

                _transaction.Commit();  // olması gereken bu 
            }
            catch 
            {
                // yapılanları geri al 
                _transaction.Rollback();
                throw;
            }
            finally
            {
                // her ne olursa olsun bu işlemleri yap anlamında => ister try a girsib ister catch'e 
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            // repositoryleri temizliyoruz.
            _productRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        // burada çok fazla if kullandık ilerleyen süreçlerde buralarında üzerinden geçmeliyiz. ne kadar az if if o kadar iyi :) s
        private void dispose(bool disposing)
        {
            if (!_dispose)
            {
                if (disposing)
                {
                    if(_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if(_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _dispose = true;
            }
        }
        ~UnitOfWork()
        {
            dispose(false);
        }

       
    }
}

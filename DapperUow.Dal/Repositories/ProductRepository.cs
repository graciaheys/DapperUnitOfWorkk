using Dapper;
using DapperUow.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DapperUow.Dal.Repositories
{
    // bu 2 sınıftan kalıtım alacağız
    internal class ProductRepository : RepositoryBase, IProductRepository
    {
        // aşağıdakileri ctrl + . ile implement ettim :)
        // referansını ekliyoruz. | her işlem işlem transaction oluşturmak istiyorum.
        public ProductRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        // alt tarafta sql cümleleri kullanacağımız için benim bunları hazırlamam lazım şimdilik bu kadar :)
        // burada hata alırsak debug'da çözeriz zaten sizede tecrübe olur. bugünlük bu kadar :)
        // şimdi Unit of work yapımızı oluşturalım.

        public void Add(Product entity)
        {
            // bu kütüphaneleri kullanmak için Dapper indirmemiz gerekiyor.
            entity.ProductId = Connection.ExecuteScalar<int>(
                "INSERT INTO PRODUCT(Name, CategoryId, Price, Weight, Height) Values(@Name, @CategoryId, @Price, @Weight, @Height); SELECT SCOPE_IDENTITY()",
                param: new {Name = entity.Name, CategoryId = entity.CategoryId, Price = entity.Price, Weight = entity.Weight, Height = entity.Height},
                transaction: Transaction
                );
        }

        public IEnumerable<Product> All()
        {
            // sizler işlemleri izlemek debug atın çalıştırdığınızda daha rahat kodu kavramanız için :)
            // TABLO ADINI KÜÇÜK YAZMIŞIZ HATALI ASLINDA BUYUK HARF OLMASI HER ZAMAN DAHA İYİ BİR  STANDART
            return Connection.Query<Product>(
                "SELECT * FROM Product",
                transaction: Transaction
                ).ToList();  // liste dönmesi gerektiği için ekledik.

            // transactionda işlem bitene kadar tabloyu kilitler ve yanlışlar olması durumda geri almaya yarar.
        }

        public void Delete(int id)
        {
            Connection.Execute(
                "DELETE FROM Product WHERE ProductId = @ProductId",
                param: new {ProductId = id},
                transaction: Transaction
                );
        }

        public void Delete(Product entity)
        {
            Delete(entity.ProductId);
        }

        public Product Find(int id)
        {
            return Connection.Query<Product>(
                "SELECT * FROM Product Where ProductId = @ProductId ",
                param: new { ProductId = id},
                transaction: Transaction
                ).FirstOrDefault();
        }

        public Product FindByName(string name)
        {
            // tek değer dönmesini beklediğimiz için FirstOrDefault
            return Connection.Query<Product>(
                "SELECT * FROM Product WHERE Name = @Name",
                param: new {Name = name },
                transaction: Transaction
                ).FirstOrDefault();
        }

        public void Update(Product entity)
        {
            Connection.Execute(
                @"UPDATE Product SET Name = @Name, 
                                     CategoryId = @CategoryId,
                                     Price = @Price,
                                     Weight = @Weight,
                                     Height = @Height
                                     WHERE ProductId = @ProductId",  // @ koymayı unutmuşuz yoksa hepsini güncellerdi :)
                param:new {Name = entity.Name, CategoryId =entity.CategoryId, Price = entity.Price, Weight= entity.Weight, Height = entity.Height, ProductId = entity.ProductId }, // ProductId = entity.ProductId   bu alanıda eklemeyi unutmayın
                transaction: Transaction
                );

            // günün yorgunlugu olunca hatalar normal :) hepsini kontrol edelim.
        }
    }
}
 
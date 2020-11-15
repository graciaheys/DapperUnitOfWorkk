using DapperUow.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperUow.Dal.Repositories
{
    // interface oluşturmalıyız.
    public interface IProductRepository
    {

        // ekleme
        void Add(Product entity);
        // liste döndürecek
        IEnumerable<Product> All();
        // id ile silme
        void Delete(int id);
        // entity ile silme
        void Delete(Product entity);
        // id ile  Arama
        Product Find(int id);
        // isim ile arama
        Product FindByName(string name);
        // Güncelle
        void Update(Product entity);

        /*
         bu interface'den kalıtım alan bir repository oluşturmalıyız.  hepsini tek tek görün diye gezdim :)
         */
    }
}

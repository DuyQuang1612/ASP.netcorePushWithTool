using App.Models;
using System.Collections.Generic;


namespace App.Services
{
    public class ProductService : List<ProductModel>
    {
        public ProductService()
        {
            this.AddRange(new ProductModel[]
            {
             new ProductModel(){Id =1, Name = "Iphone XSMax",Price=10000000},
             new ProductModel(){Id =2, Name = "Iphone 11 ProMax",Price=11000000},
             new ProductModel(){Id =3, Name = "Iphone 12 ProMax",Price=12000000},
             new ProductModel(){Id =4, Name = "Iphone 13 ProMax",Price=17500000},
            });
        }
    }
}
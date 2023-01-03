using WebshopAPI.data;

namespace WebshopAPI.lib.Services
{
    public class ProductManagerService
    {
        public List<Product> ListProducts()
        {
            using (SQL sql = new SQL())
            {
                return sql.Products.ToList();
            }
        }

        public void CreateProduct(Product product)
        {
            using (SQL sql = new SQL())
            {
                if (sql.Products.Any(x => x.Name == product.Name))
                {
                    throw new ItemAlreadyExistsException();
                }

                sql.Products.Add(product);
                sql.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SQL sql = new SQL())
            {
                if (!sql.Products.Any(x => x.ProductID == product.ProductID))
                {
                    throw new ItemNotExistsException();
                }

                Product oldProduct = sql.Products.Single(x => x.ProductID == product.ProductID);

                if (product.Available != null) oldProduct.Available = product.Available;
                if (product.Name != null) oldProduct.Name = product.Name;
                if (product.Price != null) oldProduct.Price = product.Price;
                if (product.Quantity != null) oldProduct.Quantity = product.Quantity;

                sql.SaveChanges();
            }
        }

        public void DeleteProduct(Product product)
        {
            using (SQL sql = new SQL())
            {
                if (!sql.Products.Any(x => x.ProductID == product.ProductID))
                {
                    throw new ItemNotExistsException();
                }

                sql.Remove(product);
                sql.SaveChanges();
            }
        }

        public Product GetProduct(int ProductID)
        {
            using (SQL sql = new SQL())
            {
                if (!sql.Products.Any(x => x.ProductID == ProductID))
                {
                    throw new ItemNotExistsException();
                }

                Product findedProduct = sql.Products.Single(x => x.ProductID == ProductID);
                return findedProduct;
            }
        }
    }
}

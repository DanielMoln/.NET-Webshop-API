using WebshopAPI.data;
using WebshopAPI.data.views;

namespace WebshopAPI.lib.Services
{
    public class OrdersManagerService
    {
        public List<v_OrderData> ListOrders()
        {
            using (SQL sql = new SQL())
            {
                return sql.v_OrderDatas.ToList();
            }
        }

        public void CreateOrder(OrderBody order)
        {
            using (SQL sql = new SQL())
            {
                sql.Orders.Add(new Order()
                {
                    Amount = order.Amount,
                    UserID = order.UserID,
                    ProductID = order.ProductID
                });
                sql.SaveChanges();
            }
        }

        public void DeleteOrder(int OrderID)
        {
            using (SQL sql = new SQL())
            {
                if (!sql.Orders.Any(x => x.OrderID == OrderID))
                {
                    throw new ItemNotExistsException();
                }

                Order order = sql.Orders.Single(x => x.OrderID == OrderID);
                order.Deleted = true;

                sql.SaveChanges();
            }
        }

        public void UpdateOrder(Order order)
        {
            using (SQL sql = new SQL())
            {
                if (!sql.Orders.Any(x => x.OrderID == order.OrderID))
                {
                    throw new ItemNotExistsException();
                }

                Order findedOrder = sql.Orders.Single(x => x.OrderID == order.OrderID);

                if (order.OrderDate != null) findedOrder.OrderDate = order.OrderDate;
                if (order.UserID != null) findedOrder.UserID = order.UserID;
                if (order.Deleted != null) findedOrder.Deleted = order.Deleted;
                if (order.Amount != null) findedOrder.Amount = order.Amount;

                sql.SaveChanges();
            }
        }

        public Order GetOrder(int OrderID)
        {
            using (SQL sql = new SQL())
            {
                if (!sql.Orders.Any(x => x.OrderID == OrderID))
                {
                    throw new ItemNotExistsException();
                }

                Order findedOrder = sql.Orders.Single(x => x.OrderID == OrderID);
                return findedOrder;
            }
        }
    }
}

using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public void AddOrder(Order newOrder)
		{
            // Convert new products to lookup of product
            foreach (var item in newOrder.Items)
			{
                item.Product = _ctx.Products.Find(item.Product.Id);
			}

            AddEntity(newOrder);
		}

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            try
            {
                if (includeItems)
                {
                    var ordersList = _ctx.Orders
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();

                    return ordersList;
                }
                else
                {
                    var ordersList = _ctx.Orders
                        .ToList();

                    return ordersList;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Orders: {ex}");
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            try
            {
                if (includeItems)
                {
                    var ordersList = _ctx.Orders
                        .Where(o => o.User.UserName == username)
                        .Include(o => o.Items)
                        .ThenInclude(i => i.Product)
                        .ToList();

                    return ordersList;
                }
                else
                {
                    var ordersList = _ctx.Orders
                        .Where(o => o.User.UserName == username)
                        .ToList();

                    return ordersList;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Orders: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Get All Products was called");

                return _ctx.Products
                    .OrderBy(p => p.Title)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public Order GetOrderById(string username, int id)
        {
            try
            {
                var ordersList = _ctx.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .Where(o => o.Id == id && o.User.UserName == username)
                    .FirstOrDefault();
                return ordersList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Orders: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}

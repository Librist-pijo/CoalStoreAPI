﻿using API.Repositories.Models;

namespace API.Repositories.Interfaces
{
    public interface IOrdersRepository
    {
        Task<int> Create(Orders order);
        Task Update(Orders order);
        Task Delete(int orderId);
        Task<List<Orders>> Get();
        Task<Orders> GetById(int orderId);
        Task<List<Orders>> GetOrdersByCustomerId(int customerId);
    }
}

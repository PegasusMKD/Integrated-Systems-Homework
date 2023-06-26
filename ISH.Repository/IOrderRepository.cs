﻿using ISH.Data.Orders;

namespace ISH.Repository
{
    public interface IOrderRepository
    {
        List<Order> GetAllByBoughtBy(Guid userId);
    }
}

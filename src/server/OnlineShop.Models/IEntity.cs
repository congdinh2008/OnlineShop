using System;

namespace OnlineShop.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }

        bool IsDeleted { get; set; }

        DateTime InsertedAt { get; set; }

        DateTime UpdatedAt { get; set; }
    }
}
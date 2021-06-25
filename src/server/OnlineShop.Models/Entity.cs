using System;

namespace OnlineShop.Models
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime InsertedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
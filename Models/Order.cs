﻿#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace SignalR.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemName { get; set; }
        public int Count { get; set; }
    }
}

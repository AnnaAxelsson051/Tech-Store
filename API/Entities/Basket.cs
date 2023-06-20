
using System.Collections.Generic;

namespace API.Entities
{
    public class Basket
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = new();
        //Creating a new list of items when creating a new basket
    
    //Checking if the item is already in basket, if not adding item to list
     //If item already in basket adjusting its quantity
    public void AddItem(Product product, int quantity)
    {
        if (Items.All(item => item.ProductId != product.Id))
        {
            Items.Add(new BasketItem{Product = product, Quantity = quantity});
        }
        var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id)
        if (existingItem != null) existingItem.Quantity += quantity;
    }

//Selecting an item decreasing its quantity, or removing item
    public void RemoveItem(int productId, int quantity)
    {
        var item = Items.FirstOrDefault(item => item.ProductId == productId)
        if (item == null) return;
        item.Quantity -= quantity;
        if (item.Quantity == 0) Items.Remove(item);

    }
    
    }
}
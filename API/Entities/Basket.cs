using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Basket
    {
        public int Id {get; set;}

        public string BuyerId {get; set;}

        public List<BasketItem> Items {get; set;} = new();

        public void AddItem(Product product, int quantity)
        {
            if(Items.All(item => item.ProductId != product.Id))
            {
                Items.Add(new BasketItem{Product = product, Quantity = quantity});
            }

            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingItem != null) existingItem.Quantity += quantity;

        }

        public void RemoveItem(Product product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);

            if (existingItem != null)
            {
                if (existingItem.Quantity <= quantity)
                {
                    Items.Remove(existingItem); // Remove the entire item if quantity is zero or negative
                }
                else
                {
                    existingItem.Quantity -= quantity; // Decrease quantity otherwise
                }
            }
        }
    }

    
}
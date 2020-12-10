using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Core.Entities
{
    public class CustomerBasket
    {
        // Empty constructor for REDIS
        public CustomerBasket()
        {

        }

        // No need for new List in the constructor since we are initializing a new one as automated property
        // public CustomerBasket(string id, List<BasketItem> items) => (List<BasketItem> items) as parameter === new List<BasketItem>(); GOTCHA!!!
        public CustomerBasket(string id)
        {
            Id = id;
        }

        // We let the client (Angular) generate an ID since we are using REDIS
        public string Id { get; set; }
        // We initialize the List 'cause in this situation we are not usin an ORM for this entity
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}

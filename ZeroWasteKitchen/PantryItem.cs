using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ZeroWasteKitchen
{
   public abstract class PantryItem
    {
        public string Name { get; set; }
        public string Catergory { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public PantryItem(string name, string catergory, int quantity, DateTime expirationDate)
        {
            Name = name;
            Catergory = catergory;
            Quantity = quantity;
            ExpirationDate = expirationDate;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroWasteKitchen
{
    public class FoodItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int DaysRemaining
        {
            get
            {
                return GetDaysUntilExpiration();
            }
        }

        public string Status
        {
            get
            {
                return GetStatus();
            }
        }

        public FoodItem(string name, string category, int quantity, DateTime expirationDate)
        {
            Name = name;
            Category = category;
            Quantity = quantity;
            ExpirationDate = expirationDate;
        }

        public int GetDaysUntilExpiration()
        {
            return (ExpirationDate - DateTime.Today).Days;
        }

        public string GetStatus()
        {
            int days = GetDaysUntilExpiration();

            if (days < 0)
            {
                return "Expired";
            }
            else if (days <= 3)
            {
                return "Expiring Soon";
            }
            else
            {
                return "Fresh";
            }
        }        
    }
}

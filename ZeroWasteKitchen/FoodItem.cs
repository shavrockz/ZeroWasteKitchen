using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroWasteKitchen
{
    public class FoodItem : PantryItem
    {
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

        public FoodItem(string name, string category, int quantity, DateTime expirationDate) : base(name, category, quantity, expirationDate)
        {
            
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

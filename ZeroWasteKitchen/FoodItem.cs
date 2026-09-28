using System;
using System.Collections.Generic;
using System.Text;

namespace ZeroWasteKitchen
{
    //FoodItem inherits the common pantry details from PantryItem
    //It adds the expiry calculations used by the application

    public class FoodItem : PantryItem
    {
        //Calculates the number of days remaining whenever it is requested
        public int DaysRemaining
        {
            get
            {
                return GetDaysUntilExpiration();
            }
        }

        //displays the expiry status based on the remaining days
        public string Status
        {
            get
            {
                return GetStatus();
            }
        }

        // Number of days used to decide when an item is close to expiry.
        private const int ExpiringSoonDays = 3;

        //sends the item details to the PantryItem base constructor
        public FoodItem(string name, string category, int quantity, DateTime expirationDate) : base(name, category, quantity, expirationDate)
        {
            
        }

        //this calculates how many days are left before the item expires
        public int GetDaysUntilExpiration()
        {
            return (ExpirationDate - DateTime.Today).Days;
        }

        //Overrides the base method and decides the items expiry status
        //Which is an example of polymorphism
        public override string GetStatus()
        {
            int days = GetDaysUntilExpiration();

            if (days < 0)
            {
                return "Expired";
            }
            else if (days <= ExpiringSoonDays)
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

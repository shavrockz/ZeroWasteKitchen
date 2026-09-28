using System;

namespace ZeroWasteKitchen
{
    //PantryItem is an abstract base class.
    //i created this class so that common information such as name, category,
    //quantity and expiry date does not need to be repeated in every type of pantry item.

    //because this class is abstract, objects cannot be created directly from PantryItem.
    //Other classes, such as FootItem, inherit from it.
    public abstract class PantryItem
    {
        //These properties store the basic information that every pantry item needs.
        //They are placed in the based class so child classes can reuse them through inheritance.
        public string Name { get; set; }
        public string Category { get; set; }
        private int quantity;
        public DateTime ExpirationDate { get; set; }

        // through the public property, this demonstartes encapulation
        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                if(value < 0)
                {
                    quantity = 0;
                }
                else
                {
                    quantity = value;
                }
            }
        }
       

        //The constructor receives the common details when a new item is created.
        public PantryItem(string name, string category, int quantity, DateTime expirationDate)
        {
            Name = name;
            Category = category;
            Quantity = quantity;
            ExpirationDate = expirationDate;
        }

        //Child classes must provide their own status logic.
        //This demos abstraction and polymorphism
        public abstract string GetStatus();
    }
}

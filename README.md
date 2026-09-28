ZERO-WASTE KITCHEN
Pantry Expiry Assistant


1. PROJECT OVERVIEW

Zero-Waste Kitchen is a C# Windows Forms application created for the
ITS203 Object-Oriented Design and Programming assessment.

The application helps users manage pantry and food items and keep track
of their expiry dates.

Users can add, update and delete food items, filter items by category,
and view the expiry status of each item.

The application uses colour coding to show the expiry status:

- Red = Expired
- Yellow = Expiring Soon
- Green = Fresh

The project was developed using C#, Windows Forms and JSON file storage.


2. SOFTWARE USED

The following software and technologies were used to develop and run
Zero-Waste Kitchen:

- Visual Studio
- C#
- .NET Windows Forms
- JSON
- System.Text.Json
- Git
- GitHub


3. PROJECT FILES

The main project files include:

- Program.cs
- MainForm.cs
- MainForm.Designer.cs
- FoodItem.cs
- PantryItem.cs
- inventory.json

Program.cs starts the Windows Forms application.

MainForm.cs contains the main application logic, including adding,
updating, deleting, filtering and displaying inventory items.

FoodItem.cs contains the expiry calculations and status logic.

PantryItem.cs is the abstract base class that stores common information
shared by pantry items.

inventory.json is used to store inventory data between application
sessions.


4. RUNNING THE APPLICATION

To run the application:

1. Open the ZeroWasteKitchen solution in Visual Studio.

2. Build the project.

3. Run the application using the Start button or press F5.

4. The main inventory window will appear.

5. Enter an item name, select a category, choose a quantity and expiry
   date.

6. Click the Add Item button to add the item to the inventory.


5. MAIN FEATURES

Zero-Waste Kitchen includes the following features:

- Add food items
- Update existing items
- Delete food items
- Delete confirmation message
- Category selection
- Category filtering
- Quantity tracking
- Expiry date tracking
- Days remaining calculation
- Expiry status calculation
- Colour-coded expiry status
- Automatic expiry-date sorting
- Input validation
- JSON file saving
- JSON file loading
- Data remains available after restarting the application


6. EXPIRY STATUS

The application calculates the number of days between the current date
and the item's expiry date.

Each item is given one of three expiry statuses:

Expired
The expiry date has already passed.

Expiring Soon
The item expires within 3 days.

Fresh
The item has more than 3 days remaining.

The DataGridView also uses colours to make the status easier to identify:

- Red = Expired
- Yellow = Expiring Soon
- Green = Fresh


7. OBJECT-ORIENTED PROGRAMMING

The project uses several object-oriented programming concepts.


Classes and Objects

FoodItem is used to create food item objects.

Each FoodItem object contains information such as:

- Name
- Category
- Quantity
- Expiration Date
- Days Remaining
- Status


Inheritance

FoodItem inherits from PantryItem.

PantryItem stores the common properties that are shared by pantry items.

This avoids repeating the same properties inside the FoodItem class.


Abstraction

PantryItem is an abstract class.

It contains the abstract GetStatus() method.

This means a child class must provide its own implementation of the
GetStatus() method.


Polymorphism

FoodItem overrides the GetStatus() method inherited from PantryItem.

The FoodItem version of GetStatus() decides whether the item is:

- Expired
- Expiring Soon
- Fresh

This demonstrates polymorphism because the child class provides its own
implementation of a method defined by the base class.


Encapsulation

The quantity value is stored using a private field.

The public Quantity property controls access to this value.

This helps protect the value from being changed directly and
demonstrates encapsulation.


8. INPUT VALIDATION

The application validates user input before an item can be added or
updated.

The application checks:

- Item name is not empty
- Item name does not contain only spaces
- Quantity is greater than 0
- A category has been selected

If the input is not valid, the application displays a message and stops
the operation.

Extra spaces before and after an item name are also removed using
Trim() before the item is stored.


9. JSON DATA STORAGE

The application uses JSON to save the inventory.

System.Text.Json is used to convert the List of FoodItem objects into
JSON text.

The JSON data is saved to:

inventory.json

When the application starts, it checks whether the file exists.

If the file exists, the JSON data is read and converted back into
FoodItem objects.

This allows the inventory to remain available after the application
has been closed and reopened.


10. EXCEPTION HANDLING

Exception handling is used when saving and loading inventory data.

The SaveData() and LoadData() methods use try and catch blocks.

If a file or JSON error occurs, the application displays an error
message instead of closing unexpectedly.

This improves the reliability of the application.


11. CATEGORY FILTERING

Users can filter the inventory by category.

When a category is selected, the application creates a temporary list
containing only the matching items.

The original inventory list is not changed.

Selecting All displays the complete inventory again.


12. DATA DISPLAY

Inventory items are displayed using a DataGridView.

The table displays information including:

- Name
- Category
- Quantity
- Expiration Date
- Days Remaining
- Status

The inventory is sorted by expiry date so items with the closest expiry
date appear first.

When a user selects a row, the item's details are copied into the input
fields so the item can be reviewed or updated.


13. ERROR PREVENTION

The application includes several features to reduce user errors.

These include:

- Input validation
- Delete confirmation
- Checking that an item is selected before updating
- Checking that an item is selected before deleting
- Exception handling when saving and loading data
- Preventing negative quantity values
- Removing unnecessary spaces from item names


14. KNOWN ISSUES

No major issues are currently known during normal testing.

The inventory.json file is stored locally with the application.

If the file is deleted, previously saved inventory data will no longer
be available.


15. FUTURE IMPROVEMENTS

Possible future improvements include:

- Search for an item by name
- More food categories
- Low-stock warnings
- Expiry notifications
- Automatic reminders
- Additional pantry item types
- Dashboard statistics
- Display the number of expired items
- Display the number of items expiring soon
- Export inventory data
- Improved user interface design


16. GITHUB REPOSITORY

The project source code and development history are available on GitHub:

https://github.com/shavrockz/ZeroWasteKitchen

Git was used throughout development to record changes and improvements
to the project.


17. REFERENCES AND TOOLS USED

ChatGPT was used as a study and research support tool during
the development of this project.

It was used to help explain object-oriented programming concepts,
clarify assessment requirements, provide troubleshooting guidance,
review code structure and assist with documentation.

18. PROJECT PURPOSE

The purpose of Zero-Waste Kitchen is to provide a simple way for users
to manage household food items and keep track of expiry dates.

The application aims to make it easier to identify food that has
expired or is close to expiring, which may help reduce unnecessary
food waste.

The project demonstrates object-oriented programming using C#,
including classes and objects, encapsulation, inheritance, abstraction,
polymorphism, exception handling, Windows Forms and JSON data storage.
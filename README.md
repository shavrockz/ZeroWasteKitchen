# Zero-Waste Kitchen & Pantry Expiry Assistant

## Project Overview

Zero-Waste Kitchen is a C# Windows Forms application developed for
ITS203 Object-Oriented Design and Programming.

The application helps users keep track of pantry items and expiry dates.
Users can add, update and delete items, filter by category and view
expiry status using colour coding.

Red = Expired
Yellow = Expiring Soon
Green = Fresh

Inventory data is saved locally using JSON.

## Main Features

- Add, update and delete food items
- Category filtering
- Quantity and expiry tracking
- Days remaining calculation
- Colour-coded expiry status
- Input validation
- JSON save and load
- Exception handling

## OOP Concepts

The project demonstrates:

- Classes and objects using FoodItem
- Inheritance using FoodItem : PantryItem
- Abstraction using the abstract PantryItem class
- Polymorphism using the overridden GetStatus() method
- Encapsulation using the private quantity field and Quantity property
- Exception handling using try-catch in SaveData() and LoadData()

## How to Run

1. Open ZeroWasteKitchen.slnx in Visual Studio.
2. Build the solution.
3. Press F5 to run the application.

## References and Tools Used

- Microsoft Visual Studio
- C# and .NET Windows Forms
- System.Text.Json
- Git and GitHub
- NAPS ITS203 lecture notes and tutorial materials
- ChatGPT by OpenAI was used as a study and research support tool for
  understanding OOP concepts, troubleshooting and reviewing code.
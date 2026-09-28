using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json;

namespace ZeroWasteKitchen
{
    public partial class MainForm : Form
    {

        // Main inventory dashboard
        private List<FoodItem> inventory = new List<FoodItem>();

        //File is used to save and load the inventry between sessions
        private string filePath = "inventory.json";
        public MainForm()
        {
            InitializeComponent();

            //connects the formatting event so expiry status cells can be colour coded
            dgvInventory.CellFormatting += dgvInventory_CellFormatting;

            //loads previously saved inventory when the application starts
            LoadData();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            //Checks if user has enetered valid inputs
            if (!ValidateInput())
            {
                return;
            }

            //creates a new FoodItem using the values entered by the user
            FoodItem newItem = new FoodItem(txtItemName.Text.Trim(), cmbCategory.Text, (int)numQuantity.Value, dtpExpiryDate.Value);
            inventory.Add(newItem);

            //saves the updated inventory to the JSON file
            SaveData();

            //refreshes the DataGridView to display the latest inventory
            RefreshInventoryGrid(inventory);

            //sorts items by expiry date so the closest expiry appears first
            inventory = inventory.OrderBy(item => item.ExpirationDate).ToList();            

            //clears the input fields ready for the next item
            txtItemName.Clear();
            cmbCategory.SelectedIndex = -1;
            numQuantity.Value = numQuantity.Minimum;
            dtpExpiryDate.Value = DateTime.Today;

            //displays this message once adding is completed
            MessageBox.Show("Item added successfully!");           

        }


        private void dgvInventory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //only applies color formatting to the status column
            if (dgvInventory.Columns[e.ColumnIndex].Name != "Status" || e.Value == null)
            {
                return;
            }

            string status = e.Value.ToString() ?? "";
                
            // uses different colours to make expiry conditions easy to identify
            if (status == "Expired")
            {
                    e.CellStyle.BackColor = Color.Red;
            }
            else if (status == "Expiring Soon")
            {
                    e.CellStyle.BackColor = Color.Yellow;
            }
            else if (status == "Fresh")
            {
                    e.CellStyle.BackColor = Color.LightGreen;

            }
        }
        

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            //makes sure a valid food item is selected before deleting
            if (dgvInventory.CurrentRow == null)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            FoodItem selectedItem = (FoodItem)dgvInventory.CurrentRow.DataBoundItem;

            //ask for confirmation to prevent accidental deletion
            DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }

            // removes the selected item and saves the updated inventory
            inventory.Remove(selectedItem);
            SaveData();

            //refreshes the table after the item is deleted
            RefreshInventoryGrid(inventory);

                MessageBox.Show("Item deleted successfully!");            
        }

        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            //makes sure a valid food item is slected before updating
            if (dgvInventory.CurrentRow == null)
            {
                MessageBox.Show("Please select an item to update.");
                return;
            }

            FoodItem selectedItem = (FoodItem)dgvInventory.CurrentRow.DataBoundItem;

            //checks input is valid or not
            if (!ValidateInput())
            {
                return;
            }

            // Update the selected item's properties
            selectedItem.Name = txtItemName.Text.Trim();
            selectedItem.Category = cmbCategory.Text;
            selectedItem.Quantity = (int)numQuantity.Value;
            selectedItem.ExpirationDate = dtpExpiryDate.Value;

            //keeps the inventory ordered by the closest expiry date
            inventory = inventory.OrderBy(item => item.ExpirationDate).ToList();

            SaveData();
            RefreshInventoryGrid(inventory);

            MessageBox.Show("Item updated successfully!");
        }

        //shows the full inventory when ALL is selected
        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterCategory.Text == "All")
            {
                RefreshInventoryGrid(inventory);
            }
            else
            {
                //creates a temporary list containing only items from the selected category
                var filteredItems = inventory.Where(item => item.Category == cmbFilterCategory.Text).ToList();

                //displays the filtered results without changing the original inventory list
                RefreshInventoryGrid(filteredItems);
            }
        }

        private void SaveData()
        {
            try
            {
                //converts the inventory list into JSON text
                string json = JsonSerializer.Serialize(inventory);

                //saves the JSON data so the inventory is kept after the app closes
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                //shows an error message instead of crashing if the file cannot be saved
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        private void LoadData()
        {
            try
            {
                //checks whether a saved inventory file already exists
                if (File.Exists(filePath))
                {
                    //reads the saved JSON text from the file
                    string json = File.ReadAllText(filePath);

                    //converts the JSON back into FoodItem objects
                    inventory = JsonSerializer.Deserialize<List<FoodItem>>(json) ?? new List<FoodItem>();

                    //sorts loaded items so the cloest expiry date is appeared first
                    inventory = inventory.OrderBy(item => item.ExpirationDate).ToList();

                    //displays the loaded inventory in the DataGridView
                    RefreshInventoryGrid(inventory);
                }
            }
            catch (Exception ex)
            {
                //prevents the app from crashing if loading fails
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //checks that the user clicked a valid data row
            if(e.RowIndex >= 0)
            {
                FoodItem selectedItem = (FoodItem)dgvInventory.Rows[e.RowIndex].DataBoundItem;

                //copies the selcted items details into the input controls
                //so the user can review or update them
                txtItemName.Text = selectedItem.Name;
                cmbCategory.Text = selectedItem.Category;
                numQuantity.Value = selectedItem.Quantity;
                dtpExpiryDate.Value = selectedItem.ExpirationDate;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                MessageBox.Show("Please enter an item name.");
                return false;
            }

            if(numQuantity.Value <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0.");
                return false;
            }

            if(cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.");
                return false;
            }

            return true;
        }

        private void RefreshInventoryGrid(List<FoodItem> items)
        {
            dgvInventory.DataSource = null;
            dgvInventory.DataSource = items;
        }
    }

}

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
        private string filePath = "inventory.json";
        public MainForm()
        {
            InitializeComponent();

            dgvInventory.CellFormatting += dgvInventory_CellFormatting;

            LoadData();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (txtItemName.Text == "")
            {
                MessageBox.Show("Please enter an item name.");
                return;
            }

            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("Quantity must be greater than 0.");
                return;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            FoodItem newItem = new FoodItem(txtItemName.Text, cmbCategory.Text, (int)numQuantity.Value, dtpExpiryDate.Value);
            inventory.Add(newItem);
            SaveData();

            inventory = inventory.OrderBy(item => item.ExpirationDate).ToList();

            dgvInventory.DataSource = null;
            dgvInventory.DataSource = inventory;

            txtItemName.Clear();
            cmbCategory.SelectedIndex = -1;
            numQuantity.Value = numQuantity.Minimum;
            dtpExpiryDate.Value = DateTime.Today;

            MessageBox.Show("Item added successfully!");

        }

        private void dgvInventory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvInventory.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();

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
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow == null)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            FoodItem selectedItem = (FoodItem)dgvInventory.CurrentRow.DataBoundItem;

            DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }

            inventory.Remove(selectedItem);
            SaveData();

            dgvInventory.DataSource = null;
            dgvInventory.DataSource = inventory;

                MessageBox.Show("Item deleted successfully!");            
        }

        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            if (dgvInventory.CurrentRow == null)
            {
                MessageBox.Show("Please select an item to update.");
                return;
            }

            FoodItem selectedItem = (FoodItem)dgvInventory.CurrentRow.DataBoundItem;

            // Update the selected item's properties
            selectedItem.Name = txtItemName.Text;
            selectedItem.Category = cmbCategory.Text;
            selectedItem.Quantity = (int)numQuantity.Value;
            selectedItem.ExpirationDate = dtpExpiryDate.Value;

            SaveData();

            inventory = inventory.OrderBy(item => item.ExpirationDate).ToList();

            dgvInventory.DataSource = null;
            dgvInventory.DataSource = inventory;

            MessageBox.Show("Item updated successfully!");
        }

        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterCategory.Text == "All")
            {
                dgvInventory.DataSource = null;
                dgvInventory.DataSource = inventory;
            }
            else
            {
                var filteredItems = inventory.Where(item => item.Category == cmbFilterCategory.Text).ToList();
                dgvInventory.DataSource = null;
                dgvInventory.DataSource = filteredItems;
            }
        }

        private void SaveData()
        {
            try
            {
                string json = JsonSerializer.Serialize(inventory);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    inventory = JsonSerializer.Deserialize<List<FoodItem>>(json) ?? new List<FoodItem>();

                    inventory = inventory.OrderBy(item => item.ExpirationDate).ToList();

                    dgvInventory.DataSource = null;
                    dgvInventory.DataSource = inventory;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void dgvInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                FoodItem selectedItem = (FoodItem)dgvInventory.Rows[e.RowIndex].DataBoundItem;
                txtItemName.Text = selectedItem.Name;
                cmbCategory.Text = selectedItem.Category;
                numQuantity.Value = selectedItem.Quantity;
                dtpExpiryDate.Value = selectedItem.ExpirationDate;
            }
        }
    }

}

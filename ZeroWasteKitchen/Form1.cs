using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ZeroWasteKitchen
{
    public partial class MainForm : Form
    {

        // Main inventory dashboard
        private List<FoodItem> inventory = new List<FoodItem>();
        public MainForm()
        {
            InitializeComponent();

            dgvInventory.CellFormatting += dgvInventory_CellFormatting;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (txtItemName.Text == "")
            {
                MessageBox.Show("Please enter an item name.");
                return;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            FoodItem newItem = new FoodItem(txtItemName.Text, cmbCategory.Text, (int)numQuantity.Value, dtpExpiryDate.Value);
            inventory.Add(newItem);

            inventory = inventory.OrderBy(item => item.ExpirationDate).ToList();

            dgvInventory.DataSource = null;
            dgvInventory.DataSource = inventory;


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
            if(dgvInventory.CurrentRow == null)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            FoodItem selectedItem = (FoodItem)dgvInventory.CurrentRow.DataBoundItem;

            inventory.Remove(selectedItem);

            dgvInventory.DataSource = null;
            dgvInventory.DataSource = inventory;

            MessageBox.Show("Item deleted successfully!");

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form_Residents : Form
    {
        public Form_Residents()
        {
            InitializeComponent();
            loadTable();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Info Form_Info = new Form_Info();
            Form_Info.ShowDialog();
        }

        void loadTable()
        {
            string myConnection = "server=web113.default-host.net;Port=3306;uid=daleth_max;pwd=huE96m5VHbC3;database=daleth_warehouse";
            MySqlConnection myConnect = new MySqlConnection(myConnection);

            string selectQuery = "";

            try
            {
                myConnect.Open();
                selectQuery =
                "SELECT * FROM daleth_warehouse.resident_table;";

                MySqlCommand command = new MySqlCommand(selectQuery, myConnect);

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();

                dataAdapter.SelectCommand = command;
                DataTable dTable = new DataTable();

                dataAdapter.Fill(dTable);

                BindingSource bSource = new BindingSource();

                bSource.DataSource = dTable;
                dataGridViewResidents.DataSource = bSource;
                dataAdapter.Update(dTable);

                dataGridViewResidents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                dataGridViewResidents.Columns[0].HeaderText = "ID";
                dataGridViewResidents.Columns[1].HeaderText = "Name";
                dataGridViewResidents.Columns[2].HeaderText = "Sec. name";
                dataGridViewResidents.Columns[2].HeaderText = "Address";

                myConnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void searchResident(string SearchParam, string SearchType) {
            string myConnection = "server=web113.default-host.net;Port=3306;uid=daleth_max;pwd=huE96m5VHbC3;database=daleth_warehouse";
            MySqlConnection myConnect = new MySqlConnection(myConnection);
            
            string selectQuery = "";

            if (SearchType == "Ім*я")
            {
                selectQuery = "SELECT * FROM daleth_warehouse.resident_table WHERE resident_name = '"+ SearchParam + "';";
            }
            if (SearchType == "Прізвище")
            {
                selectQuery = "SELECT * FROM daleth_warehouse.resident_table WHERE resident_sec_name = '" + SearchParam + "';";
            }


            try
            {
                myConnect.Open();

                MySqlCommand command = new MySqlCommand(selectQuery, myConnect);

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();

                dataAdapter.SelectCommand = command;
                DataTable dTable = new DataTable();

                dataAdapter.Fill(dTable);

                BindingSource bSource = new BindingSource();

                bSource.DataSource = dTable;
                dataGridViewResidents.DataSource = bSource;
                dataAdapter.Update(dTable);

                dataGridViewResidents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

                dataGridViewResidents.Columns[0].HeaderText = "ID";
                dataGridViewResidents.Columns[1].HeaderText = "Name";
                dataGridViewResidents.Columns[2].HeaderText = "Sec. name";
                dataGridViewResidents.Columns[3].HeaderText = "Address";

                myConnect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if( this.toolStripTextBoxSearch.Text != "" && this.toolStripComboBoxSearcType.Text != "")
            {
                searchResident(this.toolStripTextBoxSearch.Text, this.toolStripComboBoxSearcType.Text);
            }
        }
    }
}

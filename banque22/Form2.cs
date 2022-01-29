using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using back;

namespace banque22
{
    public partial class Form2 : Form
    {

        SqlConnection sql = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=banque;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        List<compte> co = new List<compte>();
        public Form2(int id)
        {
            InitializeComponent();
            button5.Hide();
            numericUpDown1.Hide();
            button3.Hide();
            button4.Hide();
            sql.Open();

            SqlDataReader reader = Select_com("select * from compte where idClient=" + id);
            while (reader.Read())
            {
                compte temp = new compte( int.Parse(reader["numCompte"].ToString()), float.Parse(reader["solde"].ToString()));
                       
                co.Add(temp);
            }
            reader.Close();

            sql.Close();
            foreach(compte c in co)
            {
                c.ajouter_compte(c);

                comboBox1.Items.Add(c.numCompte);

            }



        }

        private void Update_Comm(string com)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand(com, sql);
            adapter.UpdateCommand.ExecuteNonQuery();
            adapter.UpdateCommand.Dispose();
        }
        private void Delete_Com(string com)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.DeleteCommand = new SqlCommand(com, sql);
            adapter.DeleteCommand.ExecuteNonQuery();
            adapter.DeleteCommand.Dispose();
        }
        private void Insert_Com(string com)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = new SqlCommand(com, sql);
            adapter.InsertCommand.ExecuteNonQuery();
            adapter.InsertCommand.Dispose();
        }
        private SqlDataReader Select_com(string sqle)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sqle, sql);
            SqlDataReader st = adapter.SelectCommand.ExecuteReader();
            adapter.SelectCommand.Dispose();
            return st;
        }
        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int numc = int.Parse(comboBox1.SelectedItem.ToString());
           foreach(compte c in co)
            {
                if(c.numCompte == numc)
                {
                   
              dataGridView1.DataSource = c.comptes;
                }
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                  numericUpDown1.Show();

                    button3.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int numc = int.Parse(comboBox1.SelectedItem.ToString());
            int val = int.Parse(numericUpDown1.Value.ToString());
            foreach (compte c in co)
            {
                if(c.numCompte == numc)
                {

                if (val > 0)
                {
                    c.debiter(val);
                    sql.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = new SqlCommand("UPDATE compte SET solde = " + c.solde + "where  idClient = " + c.numCompte, sql);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    adapter.UpdateCommand.Dispose();
                    sql.Close();
                    MessageBox.Show("bien fait");
                    numericUpDown1.Hide();
                    button3.Hide();
                    dataGridView1.DataSource = c.comptes;
                }

                else
                    MessageBox.Show("error");

            }
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            numericUpDown1.Show();

            button5.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int numc = int.Parse(comboBox1.SelectedItem.ToString());
         
            int val = int.Parse(numericUpDown1.Value.ToString());
            foreach (compte c in co)
            {
                if(c.numCompte ==numc)
                {

                if (c.crediter(val))
                {
                    sql.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = new SqlCommand("UPDATE compte SET solde = " + c.solde + "where  idClient = " + c.numCompte, sql);
                    adapter.UpdateCommand.ExecuteNonQuery();
                    adapter.UpdateCommand.Dispose();
                    sql.Close();
                    MessageBox.Show("bien fait");
                    numericUpDown1.Hide();
                    button5.Hide();
                    dataGridView1.DataSource = c.comptes;
                }

                else
                    MessageBox.Show("error");

            }
                }
        }
    }
}

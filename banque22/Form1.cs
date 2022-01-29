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
    public partial class Form1 : Form
    {
        SqlConnection sql = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=banque;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        List<Client> clients = new List<Client>();
        public Form1()
        {
            InitializeComponent();
            sql.Open();
            SqlDataReader reader = Select_com("Select * from client"), reader1;
            while (reader.Read())
            {
                Client temp = new Client( reader["cin"].ToString(), reader["nom"].ToString(), reader["prenom"].ToString(), reader["adresse"].ToString(), reader["login"].ToString(), reader["pass"].ToString(), int.Parse(reader["Id"].ToString()));
                clients.Add(temp);
            }
            reader.Close();
            foreach (Client client in clients)
            {
                reader1 = Select_com("select * from compte where idClient=" + client.id);
                while (reader1.Read())
                {
                    client.ajouter_compte(new compte(int.Parse(reader1["numCompte"].ToString()), float.Parse(reader1["solde"].ToString())));
                }
                reader1.Close();
            }
            sql.Close();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Client client in clients)
            {
                if (client.auth(textBox1.Text, textBox2.Text))
                {
                    /*dataGridView1.DataSource = client.comptes;

                    MessageBox.Show("Loged");*/
                    new Form2(client.id).Show();
                    break;
                }
                MessageBox.Show("wrong information");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using back;
namespace compteBanque
{
    SqlConnection sql = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = banque; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    public partial class Form1 : Form
    {
           List <Client> lclient = new List<Client>();

        public Form1()
        {
            InitializeComponent();
            sql.Open();
            SqlDataReader reader = Select_com("Select * from client"), reader1;
          
            while (reader.Read())
            {
                Client temp = new Client(int.Parse(reader["id"].ToString()), reader["cin"].ToString(), reader["nom"].ToString(), reader["prenom"].ToString(), reader["address"].ToString(), reader["login"].ToString(), reader["pass"].ToString());
                lclient.Add(temp);
            }

            reader.Close();
            foreach (Client client in lclient)
            {
                reader1 = Select_com("select * from Compte where idClient=" + client.id);
                while (reader1.Read())
                {
                    client.ajouter_compte(new compte(int.Parse(reader1["numCompte"].ToString()), double.Parse(reader1["solde"].ToString())));
                }
                reader1.Close();
            }
            sql.Close();
        }
        private SqlDataReader Select_com(string sqle)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sqle, sql);
            SqlDataReader st = adapter.SelectCommand.ExecuteReader();
            adapter.SelectCommand.Dispose();
            return st;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string pass = textBox2.Text;
            
        }
    }

    
}

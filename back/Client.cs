using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace back
{
    public class Client
    {
        private string Cin, nom, prenom, adresse;
        private string login, password;
        public List<compte> comptes { get; }
        public int id;

        public Client(string cin, string nom, string prenom, string adresse, string login, string password, int id)
        {
            Cin = cin;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.login = login;
            this.password = password;
            this.comptes = new List<compte>();
            this.id = id;
        }

        public void ajouter_compte(compte co)
        {
            if(!comptes.Contains(co))
            {
                comptes.Add(co);
            }
        }


        public bool auth(string login, string pass)
        {
            return this.login == login && pass == password;
        }





    }
}

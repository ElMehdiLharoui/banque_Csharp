using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back
{
   public class compte
    {

        public int numCompte { get; }
        public Client c;
        public List<compte> comptes= new List<compte>();
        public float solde { get;  set; }
        public static int plafond = 1000;


        public compte(int numCompte, float solde)
        {
            this.numCompte = numCompte;
            this.solde = solde;
        }
        public void ajouter_compte(compte c)
        {
            if (!comptes.Contains(c))
            {

            comptes.Add(c);// ajouter toute la liste de client 
            }
        }
        public void debiter(float va)
        {

            solde+= va;
        }
        public bool crediter(float s)
        {
            if(solde >= s && s <= plafond)
            {
                solde -= s;
                return true;
            }
            return false;

        }
       

}
}

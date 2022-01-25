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
       
        public float solde { get; }


        public compte(int numCompte, float solde)
        {
            this.numCompte = numCompte;
            this.solde = solde;
        }
       

}
}

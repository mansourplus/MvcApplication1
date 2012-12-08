using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftSchool.Models
{
    public class Metier
    {
        SoftSchool.Models.dbsoftschoolEntities1 db = new SoftSchool.Models.dbsoftschoolEntities1();
        public string getContactByNom(long id)
        {
           utilisateur usr= db.utilisateur.SingleOrDefault(u => u.aspnet_user == id);
            string str = "";
            if (usr != null)
            {
                str = usr.Nom + " " + usr.Prenom;
            }
            return str;
        }

        public string getContactByLycéee(long id)
        {
            utilisateur usr = db.utilisateur.SingleOrDefault(u => u.aspnet_user == id);
            string str = "";
            if (usr != null)
            {
                str = usr.lycees.Nom;
            }
            return str;
        }
    }
}
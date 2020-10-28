using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DemoProjetGraphiqueAccesDonnees.AccesDonnees
{
    public class CommandeDao : ICommandeDao
    {
        private readonly IDbConnection _connexion;

        public CommandeDao(IDbConnection connexion)
        {
            _connexion = connexion;
        }

        public IList<Commande> ObtenirCommandesEmploye(int employeId)
        {
            string requete = @"SELECT * FROM Sales.Orders WHERE empid = @empid;";
            return _connexion.Query<Commande>(requete, new { empid = employeId }).ToList();
        }

        public int Supprimer(int orderId)
        {
            string requete = "DELETE FROM Sales.Orders WHERE orderId = @orderId;";
            return _connexion.Execute(requete, new { orderId });
        }
    }
}

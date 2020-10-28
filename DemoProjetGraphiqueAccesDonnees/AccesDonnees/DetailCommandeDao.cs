using Dapper;
using System.Data;

namespace DemoProjetGraphiqueAccesDonnees.AccesDonnees
{
    public class DetailCommandeDao : IDetailCommandeDao
    {
        private readonly IDbConnection _connexion;

        public DetailCommandeDao(IDbConnection connexion)
        {
            _connexion = connexion;
        }

        public int SupprimerParCommande(int orderId)
        {
            string requete = "DELETE FROM Sales.OrderDetails WHERE orderId = @orderId;";
            return _connexion.Execute(requete, new { orderId });
        }
    }
}

using System.Collections.Generic;

namespace DemoProjetGraphiqueAccesDonnees.AccesDonnees
{
    public interface ICommandeDao
    {
        IList<Commande> ObtenirCommandesEmploye(int employeId);

        int Supprimer(int orderId);
    }
}
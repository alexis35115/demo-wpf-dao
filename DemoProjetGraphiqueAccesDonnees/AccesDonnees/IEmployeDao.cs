using System.Collections.Generic;

namespace DemoProjetGraphiqueAccesDonnees.AccesDonnees
{
    public interface IEmployeDao
    {
        int Creer(Employe employe);
        Employe Obtenir(int id);
        IList<Employe> ObtenirEmployes();
        int MettreAJour(Employe employe);
        int Supprimer(int id);
    }
}
using DemoProjetGraphiqueAccesDonnees.AccesDonnees;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Windows;

namespace DemoProjetGraphiqueAccesDonnees
{
    public partial class MainWindow : Window
    {
        private const string MessageNombreEmployes = "Nombre d'employé(s) : {0}";
        private const string MessageNombreCommandes = "L'employé {0} possède {1} commande(s)";

        private readonly IEmployeDao _employeDao;
        private readonly ICommandeDao _commandeDao;
        private readonly IDetailCommandeDao _detailCommandeDao;

        public MainWindow(IEmployeDao employeDao,
            ICommandeDao commandeDao,
            IDetailCommandeDao detailCommandeDao)
        {

            // Laisser cette méthode en premier!
            InitializeComponent();

            _employeDao = employeDao;
            _commandeDao = commandeDao;
            _detailCommandeDao = detailCommandeDao;
        }

        private void BtnChargerEmployes_Click(object sender, RoutedEventArgs e)
        {
            var employes = _employeDao.ObtenirEmployes();

            DgEmployes.ItemsSource = employes;
            LblNombreEmployes.Content = string.Format(MessageNombreEmployes, employes.Count);

            if (employes.Any())
            {
                DgEmployes.SelectedIndex = 0;
            }
        }

        private void Dg_employes_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RemplirDataDridCommandes();
        }

        private void BtnSupprimerCommandeSelectionnee_Click(object sender, RoutedEventArgs e)
        {
            bool unEmployeEstSelectionne = DgCommandes.SelectedIndex != -1;
            if (unEmployeEstSelectionne)
            {

                int numeroCommande = ObtenirNumeroCommandeSelectionne();

                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    _detailCommandeDao.SupprimerParCommande(numeroCommande);
                    _commandeDao.Supprimer(numeroCommande);

                    scope.Complete();
                }

                RemplirDataDridCommandes();
            }
        }

        private void BtnSupprimerToutesCommandes_Click(object sender, RoutedEventArgs e)
        {
            bool uneCommandeEstSelectionnee = DgCommandes.SelectedIndex != -1;
            if (uneCommandeEstSelectionnee)
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    var commandes = (List<Commande>)DgCommandes.ItemsSource;
                    foreach (var commande in commandes)
                    {
                        _detailCommandeDao.SupprimerParCommande(commande.orderid);
                        _commandeDao.Supprimer(commande.orderid);
                    }

                    scope.Complete();
                }

                RemplirDataDridCommandes();
            }
        }

        private int ObtenirNumeroCommandeSelectionne() => ((Commande)DgCommandes.SelectedItem).orderid;

        private int ObtenirNumeroEmployeSelectionne() => DgEmployes.SelectedItem is Employe employe ? employe.empid : 0;

        private void RemplirDataDridCommandes()
        {
            var commandes = _commandeDao.ObtenirCommandesEmploye(ObtenirNumeroEmployeSelectionne());
            DgCommandes.ItemsSource = commandes;

            if (commandes.Any())
            {
                DgCommandes.SelectedIndex = 0;
            }

            lblNombresCommandes.Content = string.Format(MessageNombreCommandes,
                ObtenirNumeroEmployeSelectionne(),
                commandes.Count);
        }
    }
}

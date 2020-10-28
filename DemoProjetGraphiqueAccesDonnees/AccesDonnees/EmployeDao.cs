using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DemoProjetGraphiqueAccesDonnees.AccesDonnees
{
    public class EmployeDao : IEmployeDao
    {
        private readonly IDbConnection _connexion;

        public EmployeDao(IDbConnection connexion)
        {
            _connexion = connexion;
        }

        public Employe Obtenir(int id)
        {
            string requete = @"SELECT * FROM HR.Employees WHERE empid = @empid;";
            return _connexion.QuerySingleOrDefault<Employe>(requete, new { empid = id });
        }

        public IList<Employe> ObtenirEmployes()
        {
            string requete = @"SELECT * FROM HR.Employees;";
            return _connexion.Query<Employe>(requete).ToList();
        }

        public int Creer(Employe employe)
        {
            string requete = @"INSERT INTO HR.Employees (lastname        
                                                        ,firstname       
                                                        ,title           
                                                        ,titleofcourtesy 
                                                        ,birthdate       
                                                        ,hiredate        
                                                        ,address         
                                                        ,city            
                                                        ,region          
                                                        ,postalcode      
                                                        ,country         
                                                        ,phone           
                                                        ,mgrid)
                                                OUTPUT INSERTED.[empid]
                                                VALUES (@lastname        
                                                       ,@firstname       
                                                       ,@title           
                                                       ,@titleofcourtesy 
                                                       ,@birthdate       
                                                       ,@hiredate        
                                                       ,@address         
                                                       ,@city            
                                                       ,@region          
                                                       ,@postalcode      
                                                       ,@country         
                                                       ,@phone           
                                                       ,@mgrid);";

            return _connexion.QuerySingle<int>(requete, employe);
        }

        public int MettreAJour(Employe employe)
        {
            string requete = @"UPDATE HR.Employees 
                                  SET lastname = @lastname
                                     ,firstname = @firstname
                                     ,title = @title
                                     ,titleofcourtesy = @titleofcourtesy
                                     ,birthdate = @birthdate
                                     ,hiredate = @hiredate
                                     ,address = @address
                                     ,city = @city
                                     ,region = @region
                                     ,postalcode = @postalcode
                                     ,country = @country
                                     ,phone = @phone
                                     ,mgrid = @mgrid 
                                WHERE empid = @empid;";

            return _connexion.Execute(requete, employe);
        }
        public int Supprimer(int id)
        {
            string requete = "DELETE FROM HR.Employees WHERE empid = @empid;";
            return _connexion.Execute(requete, new { empid = id });
        }
    }
}

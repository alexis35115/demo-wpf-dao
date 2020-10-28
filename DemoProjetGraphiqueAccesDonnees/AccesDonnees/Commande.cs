using System;

namespace DemoProjetGraphiqueAccesDonnees.AccesDonnees
{
    public class Commande
    {
#pragma warning disable IDE1006 // Styles d'affectation de noms
        public int orderid { get; set; }
        public int custid { get; set; }
        public int empid { get; set; }
        public DateTime orderdate { get; set; }
        public DateTime requireddate { get; set; }
        public DateTime shippeddate { get; set; }
        public int shipperid { get; set; }
        public float freight { get; set; }
        public string shipname { get; set; }
        public string shipaddress { get; set; }
        public string shipcity { get; set; }
        public string shipregion { get; set; }
        public string shippostalcode { get; set; }
        public string shipcountry { get; set; }
#pragma warning restore IDE1006 // Styles d'affectation de noms
    }
}

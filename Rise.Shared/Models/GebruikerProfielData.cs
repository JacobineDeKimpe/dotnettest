using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise.Shared.Models
{
    public class GebruikerProfielData
    {
        public Gebruiker Gebruiker { get; set; }
        public List<Product> Producten { get; set; }
    }

    public class Gebruiker
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string TelNr { get; set; }

    }

    public class Product
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int AantalInStock { get; set; }
    }
}

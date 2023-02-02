namespace MVC23.Models
{
    public class MarcaModelo
    {
        public int ID { get; set; }
        public string NomMarca { get; set; }

        public List<SerieModelo> Series { get; set; }


    }
}

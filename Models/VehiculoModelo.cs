namespace MVC23.Models
{
    public class VehiculoModelo
    {
        public int ID { get; set; }
        public string Matricula { get; set; }

        public string color { get; set; }

        public SerieModelo Serie { get; set; }

        public int SerieID { get; set; }  



    }
}

namespace MVC23.Models
{
    public class VehiculoExtraModelo
    {

        public int ID { get; set; }

        public VehiculoModelo Vehiculo { get; set; }

        public int VehiculoID { get; set; }

        public ExtraModelo Extra { get; set; }

        public int ExtraID { get; set; }



    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVC23.Models
{
    public class ExtraModelo 
    {
        public int ID { get; set; }

        public string TipoExtra { get; set; }
            
    }
}

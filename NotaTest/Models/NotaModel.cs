using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NotaTest.Models
{
    public class NotaModel
    {

        public int IdNota { get; set; }

        [Required(ErrorMessage ="El campo Titulo es obligatorio")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo Cuerpo es obligatorio")]
        public string? Cuerpo { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "", ApplyFormatInEditMode = true)]
        public DateTime? Fecha { get; set; }
    }
}

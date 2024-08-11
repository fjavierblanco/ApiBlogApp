using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Modelos.Dtos
{
    public class PostActualizarDto
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "El titulo es obligatorio")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        public string Descripcion { get; set; }

        public string? RutaImagen { get; set; }// ? permite valores nulos
        [Required(ErrorMessage = "La etiqueta es obligatoria")]
        public string Etiquetas { get; set; }

        //public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime FechaActualizacion { get; set; }

    }
}

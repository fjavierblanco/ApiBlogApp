using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Modelos
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        
        public string? RutaImagen { get; set; }// ? permite valores nulos
        [Required]
        public string Etiquetas { get; set; }

        public DateTime FechaCreacion {  get; set; } = DateTime.Now;

        public DateTime FechaActualizacion { get; set; }


    }
}

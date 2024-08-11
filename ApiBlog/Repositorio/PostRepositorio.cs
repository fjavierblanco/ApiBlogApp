using ApiBlog.Data;
using ApiBlog.Modelos;
using ApiBlog.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace ApiBlog.Repositorio
{
    // esta clase implementa la interface pero sin la i

    public class PostRepositorio : IPostRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public PostRepositorio(ApplicationDbContext bd)
        {
            _bd = bd; // inyectamos el DBContext mediante la interface
        }
        public bool ActualizarPost(Post post)
        {
            post.FechaActualizacion = DateTime.Now;//pasamos la fecha la modelo post
            var imagenDesdeBd = _bd.Post.AsNoTracking().FirstOrDefault(c => c.Id == post.Id);//evitar quite la imagen si yo no lo cambio por que solo actualizo el título
            if (post.RutaImagen == null)
            {
                post.RutaImagen = imagenDesdeBd.RutaImagen;// si la ruta imagen es null so pone la misma que ya existe a rutaimagen
               
            }

            _bd.Post.Update(post);
            return Guardar();
        }

        public bool BorrarPost(Post post)
        {
            _bd.Post.Remove(post);
            return Guardar();

        }

        public bool CrearPost(Post post)
        {
            post.FechaCreacion = DateTime.Now;
            _bd.Post.Add(post); // el modelo post que estamos recibiendo
            return Guardar();
        }

        public bool ExistePost(string nombre)
        {
            bool valor = _bd.Post.Any(c => c.Titulo.ToLower().Trim() == nombre.ToLower().Trim());//buscamos cualquier pos con el mismo titulo en minusculas y si espacios tolower y trim

            return valor;
        }

        public bool ExistePost(int id)
        {
            return _bd.Post.Any(c => c.Id == id);//retornamos el id si existe en la base
        }

        public Post GetPost(int postId)
        {
            return _bd.Post.FirstOrDefault(c => c.Id == postId);//traemos el post por su postId de la peticion
        }

        public ICollection<Post> GetPosts()
        {
            return _bd.Post.OrderBy(c => c.Id).ToList(); // trae una lista de todos los post por su id
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;// guarda los cambios si existe un registro o es mayor de 0
        }
    }
}

using ApiBlog.Modelos;
using ApiBlog.Modelos.Dtos;
using ApiBlog.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace ApiBlog.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepositorio _postRepo;
        private readonly IMapper _mapper;

        public PostsController(IPostRepositorio postRepo, IMapper mapper)// creamos la inyeccion de dependencias mapper y Ipost
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public IActionResult GetPosts()
        {
            var listaPosts = _postRepo.GetPosts();

            var listaPostsDto = new List<PostDto>();

            foreach (var lista in listaPosts)
            {

                listaPostsDto.Add(_mapper.Map<PostDto>(lista));// pasamos todos los datos del modelo listaPost al dto (postdto)

            }

            return Ok(listaPostsDto);// mostramos todos los datos del DTO



        }


        [HttpGet("{postId:int}", Name ="GetPost")]// capturamos el id y se los pasamos al metodo GetPost
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult GetPost(int postId)
        {
           var itemPost = _postRepo.GetPost(postId);// Traemos el posst con el metodo GetPost y el id de postId

           if (itemPost == null)
            {
                return NotFound();
                     
            }

            var itemPostDto = _mapper.Map<PostDto>(itemPost);// mapeamos datos al DTO

            return Ok(itemPostDto);// mostramos todos los datos del DTO



        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PostCrearDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public IActionResult CrearPost([FromBody] PostCrearDto CrearPostDto)// capturamos de la api el JSON con FromBody
        {
            

            if (!ModelState.IsValid) // si el modelo del estado no es valido o nos evian campos vacios obligatorios
            {
                return BadRequest(ModelState);

            }
            if (CrearPostDto == null) // si el modelo del estado no es valido o nos evian campos vacios obligatorios
            {
                return BadRequest(ModelState);

            }

            if (_postRepo.ExistePost(CrearPostDto.Titulo))
            {
                ModelState.AddModelError("", "El post ya existe");
                return StatusCode(404, ModelState);
            }
             var post = _mapper.Map<Post>(CrearPostDto);// mapeamos el CrearPostdto a la base modelo Post
            if (!_postRepo.CrearPost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro{post.Titulo}");
                return StatusCode(500, ModelState);


            }

            return CreatedAtRoute("GetPost", new { id = post.Id }); 



        }
        [HttpPatch("{postId:int}", Name = "ActualizaPatchPost")]
        [ProducesResponseType(201, Type = typeof(PostCrearDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public IActionResult ActualizarPatchPost(int postId,[FromBody] PostActualizarDto actualizarPostDto)// capturamos de la api el JSON con FromBody
        {


            if (!ModelState.IsValid) // si el modelo del estado no es valido o nos evian campos vacios obligatorios
            {
                return BadRequest(ModelState);

            }
            if (actualizarPostDto == null || postId != actualizarPostDto.Id) // si el modelo del estado no es valido o nos evian campos vacios obligatorios
            {
                return BadRequest(ModelState);

            }

            if (_postRepo.ExistePost(actualizarPostDto.Titulo))
            {
                ModelState.AddModelError("", "El post ya existe");
                return StatusCode(404, ModelState);
            }

            var post = _mapper.Map<Post>(actualizarPostDto);// mapeamos el actualizarPostdto a la base modelo Post

            if (!_postRepo.ActualizarPost(post))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro{post.Titulo}");
                return StatusCode(500, ModelState);


            }

            return NoContent();



        }



    }
}

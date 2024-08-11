using System.Net;

namespace ApiBlog.Modelos
{
    public class RespuestasAPI
    {
        public RespuestasAPI()// caoturar todas respuestas de servidor de Status Code
        {
            ErrorMessages = new List<string>();
            
        }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }// capturamos todos los errores de la api

        public object Result { get; set; }



    }
}

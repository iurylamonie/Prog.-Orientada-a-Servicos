using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace REST.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        [AcceptVerbs("POST")]
        [Route("Inserir")]
        public void Inserir([FromBody] string conteudo)
        {
            Models.CelularDataContext cdc = new Models.CelularDataContext();
            Models.Usuario usuario = JsonConvert.DeserializeObject<Models.Usuario>(conteudo);
            var r = from u in cdc.Usuarios
                    where string.Equals(u.Nome, usuario.Nome)
                    select u;
            if (r == null)
            {
                cdc.Usuarios.InsertOnSubmit(usuario);
            }
            else
            {
                var x = r.FirstOrDefault();
            }
            cdc.SubmitChanges();
        }
    }
}

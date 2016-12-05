using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace WebService.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        //Verifica se o usuario é cadastrado
        [AcceptVerbs("GET")]
        [Route("RetornarNome/{nome}")]
        public string RetornarNome(string nome)
        {           
            try
            {
                Models.IFsAppDataContext ifdc = new Models.IFsAppDataContext();
                var r = (from u in ifdc.Usuarios
                         where u.Nome == nome
                         select u).Single();
                return r.Nome;
            }
            catch (Exception)
            {

                return null;
            }
        }

        [AcceptVerbs("POST")]
        [Route("Inserir")]
        public void Inserir([FromBody] string conteudo)
        {
            Models.IFsAppDataContext ifdc = new Models.IFsAppDataContext();
            Models.Usuario usuario = JsonConvert.DeserializeObject<Models.Usuario>(conteudo);
            ifdc.Usuarios.InsertOnSubmit(usuario);
            ifdc.SubmitChanges();
        }

        [AcceptVerbs("PUT")]
        [Route("Atualizar/{nome}")]
        public void Atualizar(string nome, [FromBody] string conteudo)
        {
            Models.IFsAppDataContext ifdc = new Models.IFsAppDataContext();
            Models.Usuario usuario = JsonConvert.DeserializeObject<Models.Usuario>(conteudo);
            var r = (from u in ifdc.Usuarios
                     where u.Nome == nome
                     select u).Single();
            r.Uri = usuario.Uri;
            ifdc.SubmitChanges();
        }
    }
}

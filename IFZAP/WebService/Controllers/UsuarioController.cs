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
        [AcceptVerbs("POST")]
        [Route("Criar")]
        public void Criar([FromBody] string conteudo)
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            var usuario = JsonConvert.DeserializeObject<Models.Usuario>(conteudo);
            dc.Usuarios.InsertOnSubmit(usuario);
            dc.SubmitChanges();
        }
        [AcceptVerbs("GET")]
        [Route("Listar")]
        public List<Models.Usuario> Listar()
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            var usuarios = from u in dc.Usuarios select u;
            return usuarios.ToList();
        }
        [AcceptVerbs("GET")]
        [Route("ConsultarPorNome/{nome}")]
        public string ConsultarPorNome(string nome)
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            try
            {
                var r = (from u in dc.Usuarios where u.Nome == nome select u.Nome).Single();
                return r;
            }
            catch (Exception)
            {

                return null;
            }
            

        }
        [AcceptVerbs("PUT")]
        [Route("Alterar/{nome}")]
        public void Alterar(string nome, [FromBody] string conteudo)
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            var usuario = JsonConvert.DeserializeObject<Models.Usuario>(conteudo);
            var r = (from u in dc.Usuarios
                     where u.Nome == nome
                     select u).Single();
            r = usuario;
            dc.SubmitChanges();
        }
        [AcceptVerbs("DELETE")]
        [Route("Deletar/{id}")]
        public void Deletar(int id)
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            var usuario = (from u in dc.Usuarios where u.Id == id select u).Single();
            dc.Usuarios.DeleteOnSubmit(usuario);
            dc.SubmitChanges();
        }
    }
}

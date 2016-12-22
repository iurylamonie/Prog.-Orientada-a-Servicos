using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace WebService.Controllers
{
    [RoutePrefix("api/Grupo")]
    public class GrupoController : ApiController
    {
        [AcceptVerbs("POST")]
        [Route("Criar")]
        public void Criar([FromBody] string conteudo)
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            var grupo = JsonConvert.DeserializeObject<Models.Grupo>(conteudo);
            dc.Grupos.InsertOnSubmit(grupo);
            dc.SubmitChanges();
        }
        [AcceptVerbs("GET")]
        [Route("Listar")]
        public List<Models.Grupo> Listar()
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            var grupos = from u in dc.Grupos select u;
            return grupos.ToList();
        }
        [AcceptVerbs("GET")]
        [Route("ConsultarPorDescricao/{descricao}")]
        public string ConsultarPorNome(string descricao)
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            try
            {
                var r = (from u in dc.Grupos where u.Descricao == descricao select u.Descricao).Single();
                return r;
            }
            catch (Exception)
            {

                return null;
            }


        }
        [AcceptVerbs("PUT")]
        [Route("Alterar/{id}")]
        public void Alterar(int id, [FromBody] string conteudo)
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            var grupo = JsonConvert.DeserializeObject<Models.Grupo>(conteudo);
            var r = (from u in dc.Grupos
                     where u.Id == id
                     select u).Single();
            r = grupo;
            dc.SubmitChanges();
        }
        [AcceptVerbs("DELETE")]
        [Route("Deletar/{descricao}")]
        public void Deletar(string descricao)
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            var grupo = (from u in dc.Grupos where u.Descricao == descricao  select u).Single();
            dc.Grupos.DeleteOnSubmit(grupo);
            dc.SubmitChanges();
        }
    }
}

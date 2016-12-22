﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace WebService.Controllers
{
    [RoutePrefix("api/Membro")]
    public class MembroController : ApiController
    {
        [AcceptVerbs("POST")]
        [Route("Criar")]
        public void Criar([FromBody] string conteudo)
        {
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            var membro = JsonConvert.DeserializeObject<Models.Membro>(conteudo);
            dc.Membros.InsertOnSubmit(membro);
            dc.SubmitChanges();
        }
        [AcceptVerbs("GET")]
        [Route("ListarMembros")]
        public List<Models.Usuario> ListarMembros(string grupoDescricao)
        {
            
            Models.IFZAPDataContext dc = new Models.IFZAPDataContext();
            int grupoId = (from g in dc.Grupos
                           where g.Descricao == grupoDescricao
                           select g.Id).Single();
            var membros = from m in dc.Membros
                          where m.Grupo_Id == grupoId
                          select m;

            List<Models.Usuario> rMembros = new List<Models.Usuario>(); 
            foreach (var item in membros)
            {
                var membro = (from u in dc.Usuarios
                              where u.Id == item.Usuario_Id
                              select u).Single();
                rMembros.Add(membro);
            }

            return rMembros;
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
            var grupo = (from u in dc.Grupos where u.Descricao == descricao select u).Single();
            dc.Grupos.DeleteOnSubmit(grupo);
            dc.SubmitChanges();
        }
    }
}

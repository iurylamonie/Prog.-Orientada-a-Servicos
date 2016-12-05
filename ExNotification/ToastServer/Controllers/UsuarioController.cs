using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ToastServer.Controllers
{
    public class UsuarioController : ApiController
    {
        // GET api/usuario
        public IEnumerable<Models.User> Get()
        {
            Models.ToastDataContext dc = new Models.ToastDataContext();
            var r = from f in dc.Usuarios
                    select new Models.User { Id = f.Id, Nome = f.Nome, Uri = f.Uri };
            return r.ToList();
        }

        // POST api/Usuario
        public void Post([FromBody] string value)
        {
            Models.User user = JsonConvert.DeserializeObject<Models.User>(value);
            Models.ToastDataContext dc = new Models.ToastDataContext();
            //dc.Usuarios.InsertOnSubmit(
            //    new Models.Usuario { Nome = user.Nome, Url = user.Url });
            // Verifica se usuário existe
            var listUser = from f in dc.Usuarios
                           where string.Equals(f.Nome, user.Nome)
                           select f;
            if (listUser.Count() == 0)
            {
                dc.Usuarios.InsertOnSubmit(
                    new Models.Usuario { Nome = user.Nome, Uri = user.Uri });
            }
            else
            {
                Models.Usuario x = listUser.FirstOrDefault();
                x.Uri = user.Uri;
            }
            dc.SubmitChanges();
        }

        // PUT api/Usuario/5
        public void Put(int id, [FromBody] string value)
        {
            Models.User user = JsonConvert.DeserializeObject<Models.User>(value);
            Models.ToastDataContext dc = new Models.ToastDataContext();
            var x =
                (from f in dc.Usuarios
                 where f.Id == id
                 select f).Single();
            x.Nome = user.Nome;
            x.Uri = user.Uri;
            dc.SubmitChanges();
        }

        // DELETE api/Usuario/5
        public void Delete(int id)
        {
            Models.ToastDataContext dc = new Models.ToastDataContext();
            var x = (from f in dc.Usuarios
                     where f.Id == id
                     select f).Single();
            dc.Usuarios.DeleteOnSubmit(x);
            dc.SubmitChanges();
        }
    }
}

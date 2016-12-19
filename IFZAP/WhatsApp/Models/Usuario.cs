using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace WhatsApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Uri { get; set; }
        public System.Data.Linq.Binary Foto { get; set; }
        private HttpClient httpClient;

        private void IniciarHttp()
        {
            httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("http://localhost:61740/");
        }

        public async void CriarUsuario(string _nome, string _uri)
        {
            IniciarHttp();
            var usuario = new Usuario { Nome = _nome, Uri = _uri, Foto =  };
            string s = "=" + JsonConvert.SerializeObject(usuario);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PostAsync("api/Usuario/Criar", content);
        }
    }
}

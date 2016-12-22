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
        public string Foto { get; set; }
        private HttpClient httpClient;

        private void IniciarHttp()
        {
            httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("http://localhost:61740/");
        }
        /*public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}", Id, no);
        }*/
        public async void CriarUsuario(string _nome, string _uri)
        {
            IniciarHttp();
            var usuario = new Usuario { Nome = _nome, Uri = _uri, Foto = "A ser implementado" };
            string s = "=" + JsonConvert.SerializeObject(usuario);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PostAsync("api/Usuario/Criar", content);
        }
        public bool VerificarUsuario(string _nome)
        {
            IniciarHttp();
            var response = httpClient.GetAsync("api/Usuario/ConsultarPorNome/" + _nome);
            HttpResponseMessage rm = response.Result;
            string s = rm.Content.ReadAsStringAsync().Result;
            var str = JsonConvert.DeserializeObject<string>(s);
            if (str == _nome)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void AtualizarUsuario(string _nome, string _uri)
        {
            IniciarHttp();
            var usuario = new Usuario { Nome = _nome, Uri = _uri, Foto = "A ser implementado" };
            string s = "=" + JsonConvert.SerializeObject(usuario);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PutAsync("api/Usuario/Alterar/" + _nome, content);
        }

        public async Task<List<Usuario>> Listar()
        {
            IniciarHttp();
            HttpResponseMessage rm = await httpClient.GetAsync("api/Usuario/Listar");
            var str = rm.Content.ReadAsStringAsync().Result;
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(str);
            return usuarios.ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace WhatsApp.Models
{
    class Grupo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int IdAdm { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", Descricao); 
        }
        private HttpClient httpClient;

        private void IniciarHttp()
        {
            httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("http://localhost:61740/");
        }

        public async void Criar(Grupo _grupo)
        {
            IniciarHttp();
            string str = "=" + JsonConvert.SerializeObject(_grupo);
            var content = new StringContent(str, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PostAsync("api/Grupo/Criar", content);

        }
        public async Task<List<Models.Grupo>> Listar()
        {
            IniciarHttp();
            HttpResponseMessage rm = await httpClient.GetAsync("api/Grupo/Listar");
            string str = rm.Content.ReadAsStringAsync().Result;
            var grupos = JsonConvert.DeserializeObject<List<Models.Grupo>>(str);
            return grupos.ToList();
        }
        public async void Deletar(string descricao)
        {
            IniciarHttp();
            await httpClient.DeleteAsync("api/Grupo/Deletar/" + descricao);
        }
        public async void Alterar(int id, string _descricao)
        {
            IniciarHttp();
            Grupo grupo = new Grupo { Descricao = _descricao };
            string s = "=" + JsonConvert.SerializeObject(grupo);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PutAsync("api/Grupo/Alterar/" + id, content);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WhatsApp.Models
{
    class Membro
    {
        public int Grupo_Id { get; set; }
        public int Usuario_Id { get; set; }

        private HttpClient httpClient;

        private void IniciarHttp()
        {
            httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri("http://localhost:61740/");
        }

        public async void Criar(Membro _membro)
        {
            IniciarHttp();
            var s = "=" + JsonConvert.SerializeObject(_membro);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PostAsync("api/Membro/Criar", content);
        }

    }
}

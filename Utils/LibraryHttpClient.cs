using LibraryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LibraryUtils
{
    public class LibraryHttpClient
    {
        public HttpClient Client { get; }

        public LibraryHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net/api/v1/");
            client.DefaultRequestHeaders.Add("Accept", "text/plain");

            Client = client;
        }

        public async Task<IEnumerable<DTOUser>> GetUsers()
        {
            return await Client.GetFromJsonAsync<IEnumerable<DTOUser>>(
              "Users");
        }
    }
}

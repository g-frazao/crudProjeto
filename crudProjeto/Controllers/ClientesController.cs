using System.Net.Http;
using System.Threading.Tasks;
using crudProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientesController.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ClientesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Action para exibir a página de criação de cliente
        public IActionResult Create()
        {
            return View();
        }

        // Action para processar a criação de um novo cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient();
                var response = await client.GetAsync($"https://viacep.com.br/ws/{cliente.Cep}/json/");
                if (response.IsSuccessStatusCode)
                {
                    var enderecoJson = await response.Content.ReadAsStringAsync();
                    var endereco = JsonConvert.DeserializeObject<Endereco>(enderecoJson);
                    cliente.Logradouro = endereco.Logradouro;
                    cliente.Bairro = endereco.Bairro;
                    cliente.Cidade = endereco.Localidade;
                    cliente.Estado = endereco.Uf;

                    // Código para salvar o cliente no banco de dados aqui

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(cliente);
        }

        // Action para exibir a página de edição de cliente
        public IActionResult Edit(int id)
        {
            // Lógica para carregar o cliente do banco de dados e passá-lo para a view de edição
            return View();
        }

        // Action para processar a edição de um cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Código para atualizar o cliente no banco de dados aqui

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    // Tratamento de erro aqui
                    return View(cliente);
                }
            }
            return View(cliente);
        }

        // Action para exibir a página de confirmação de exclusão de cliente
        public IActionResult Delete(int id)
        {
            // Lógica para carregar o cliente do banco de dados e passá-lo para a view de confirmação de exclusão
            return View();
        }

        // Action para processar a exclusão de um cliente
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Código para excluir o cliente do banco de dados aqui

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Tratamento de erro aqui
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

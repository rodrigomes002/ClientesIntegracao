using Amazon.DynamoDBv2.DataModel;
using ClientesIntegracao.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientesIntegracao.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IDynamoDBContext _dynamoDBContext;

        public ClienteController(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _dynamoDBContext.ScanAsync<Cliente>(default).GetRemainingAsync();
            return View(clientes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Cnpj,TokenEgestor,TokenMovidesk")] ClienteRequest request)
        {
            var cliente = new Cliente(request.Nome, request.Cnpj, request.TokenEgestor, request.TokenMovidesk);
            await _dynamoDBContext.SaveAsync(cliente);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
                return NotFound();

            var cliente = await _dynamoDBContext.LoadAsync<Cliente>(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,Cnpj,TokenEgestor,TokenMovidesk")] Cliente cliente)
        {
            var clienteDB = await _dynamoDBContext.LoadAsync<Cliente>(id);

            if (clienteDB.Id == id)
            {
                await _dynamoDBContext.SaveAsync(cliente);
                return RedirectToAction(nameof(Index));
            }

            return View(cliente);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _dynamoDBContext.LoadAsync<Cliente>(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var cliente = _dynamoDBContext.DeleteAsync<Cliente>(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

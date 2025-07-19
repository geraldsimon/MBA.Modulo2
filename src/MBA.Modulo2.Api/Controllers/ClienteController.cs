using AutoMapper;
using MBA.Modulo2.Api.ViewModels;
using MBA.Modulo2.Business.Services.Interface;
using MBA.Modulo2.Data.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MBA.Modulo2.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]

    public class ClienteController(IClienteService clienteService,
                                  IMapper mapper,
                                  INotifier notifier) : MainController(notifier)
    {

        private readonly IClienteService _clienteService = clienteService;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<ActionResult<ClienteViewModel>> Add([FromBody] ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteService.AddAsync(_mapper.Map<Cliente>(clienteViewModel));

            return CustomResponse(clienteViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, ClienteViewModel ClienteViewModel)
        {
            if (id != ClienteViewModel.Id)
            {
                ReportError("The IDs provided are not the same!");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);


            var ClienteUpdate = await GeClienteByID(id);

            if (ClienteUpdate == null)
            {
                ReportError("Only the user who created it can make changes.");
                return CustomResponse();
            }

            ClienteUpdate.Name = ClienteViewModel.Name;

            await _clienteService.UpdateAsync(_mapper.Map<Cliente>(ClienteUpdate));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> GetByID(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await GeClienteByID(id));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Delete(Guid id)
        {

            var cliente = await GeClienteByID(id);

            if (cliente == null)
            {
                ReportError("Only the user who created it can delete the record.");
                return CustomResponse();
            }

            await _clienteService.DeleteAsync(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        private async Task<Cliente> GeClienteByID(Guid id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
            {
                ReportError("Client not found.");
                CustomResponse();
            }
            return cliente;
        }
    }
}

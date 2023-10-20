using LocadoraCarros.Application.Veiculos.Comandos.Criar;
using LocadoraCarros.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraCarros.Api.Controllers
{
    [ApiController]
    [Route("api/veiculo")]
    public class VeiculoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VeiculoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<VeiculoViewModel>> Criar(CriarVeiculoComando comando)
        {
            return Ok(await _mediator.Send(comando));
        }
    }
}

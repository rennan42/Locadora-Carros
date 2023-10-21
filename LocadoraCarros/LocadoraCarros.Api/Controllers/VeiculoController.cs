using LocadoraCarros.Application.Veiculos.Comandos.AtualizarStatus;
using LocadoraCarros.Application.Veiculos.Comandos.Criar;
using LocadoraCarros.Application.Veiculos.Comandos.Remover;
using LocadoraCarros.Application.Veiculos.Consultas.BuscarPorPlaca;
using LocadoraCarros.Application.Veiculos.Consultas.ListarVeiculosPorModelo;
using LocadoraCarros.Application.Veiculos.Consultas.ListarVeiculosPorStatus;
using LocadoraCarros.Application.ViewModels;
using LocadoraCarros.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        [HttpGet("modelo/{modelo}")]
        public async Task<ActionResult<IList<VeiculoViewModel>>> ListarPorModelo(EModeloVeiculo modelo)
        {
            var resposta = await _mediator.Send(new ListarVeiculosPorModeloConsulta { Modelo = modelo });

            if (resposta.IsNullOrEmpty())
                return NoContent();

            return Ok(resposta);
        }

        [HttpGet("placa/{placa}")]
        public async Task<ActionResult<VeiculoViewModel>> BuscarPorPlaca(string placa)
        {
            var resposta = await _mediator.Send(new BuscarVeiculoPorPlacaConsulta { Placa = placa });

            if (resposta is null)
                return BadRequest("Não foi possível concluir a ação.");

            return Ok(resposta);
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IList<VeiculoViewModel>>> BuscarPorPlaca(EStatusVeiculo status)
        {
            var resposta = await _mediator.Send(new ListarVeiculosPorStatusConsulta { Status = status });

            if (resposta.IsNullOrEmpty())
                return BadRequest("Não foi possível concluir a ação.");

            return Ok(resposta);
        }

        [HttpPatch("status")]
        public async Task<ActionResult<AtualizarStatusVeiculoComando>> AtualizarStatus(AtualizarStatusVeiculoComando comando)
        {
            var resposta = await _mediator.Send(comando);

            if (resposta)
                return Ok(comando);

            return BadRequest("Não foi possível concluir a ação.");
        }

        [HttpDelete("remover/{id}")]
        public async Task<ActionResult> Remover(long id)
        {
            await _mediator.Send(new RemoverVeiculoComando { Id = id });
            return NoContent();
        }
    }
}

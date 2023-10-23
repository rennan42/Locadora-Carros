using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net;
using LocadoraCarros.Application.Veiculos.Comandos.Criar;
using LocadoraCarros.Domain.Enum;
using LocadoraCarros.Domain.Entidades;
using FluentAssertions;
using LocadoraCarros.Tests.Shared.Builders;
using LocadoraCarros.Api.Configuracao;
using LocadoraCarros.Application.Veiculos.Comandos.AtualizarStatus;
using System.Text;
using LocadoraCarros.Application.ViewModels;

namespace LocadoraCarros.IntegrationTests.Controllers
{
    public class VeiculoControllerTestes : IntegrationTestesFixture
    {
        #region Criar Veiculo
        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá criar um veículo.")]
        public async Task DeveraCriarUmVeiculo()
        {
            var request = new CriarVeiculoComando
            {
                DataCadastro = DateTime.Now.AddMinutes(10),
                Modelo = EModeloVeiculo.SEDAN,
                Placa = "RIO2C55",
                Status = EStatusVeiculo.ALUGADO
            };

            var response = await Client.PostAsJsonAsync("/api/veiculo", request);
            var result = response.Content.ReadAsStringAsync().Result;
            var veiculoCriado = JsonConvert.DeserializeObject<Veiculo>(result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            veiculoCriado.Should().NotBeNull();
            veiculoCriado.Id.Should().BeGreaterThan(0);
        }
        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá Retornar mensagem de erro de modelo não permitido.")]
        public async Task DeveraRetornarMensagemErroModeloNaoPermitido()
        {
            var request = new CriarVeiculoComando
            {
                DataCadastro = DateTime.Now.AddMinutes(10),
                Modelo = (EModeloVeiculo)5,
                Placa = "RIO2C55",
                Status = EStatusVeiculo.ALUGADO
            };
            var respostaEsperada = new List<string>
            {
                "Modelo não permitido, escolha um modelo válido: 1-Hatch, 2-Sedan, 3-SUV.",
            };
            var response = await Client.PostAsJsonAsync("/api/veiculo", request);

            ValidarMensagemErro(respostaEsperada, response);
        }

        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá Retornar mensagem de erro formato de placa inválido.")]
        public async Task DeveraRetornarMensagemErroFormatoPlacaInvalida()
        {
            var request = new CriarVeiculoComando
            {
                DataCadastro = DateTime.Now.AddMinutes(10),
                Modelo = EModeloVeiculo.SUV,
                Placa = "RIO2555",
                Status = EStatusVeiculo.ALUGADO
            };
            var response = await Client.PostAsJsonAsync("/api/veiculo", request);
            var respostaEsperada = new List<string>
            {
                "Formato da placa inválido.",
            };

            ValidarMensagemErro(respostaEsperada, response);
        }

        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá Retornar mensagem de erro Data inferior a atual.")]
        public async Task DeveraRetornarMensagemErroDataInferiorAtual()
        {
            var request = new CriarVeiculoComando
            {
                DataCadastro = DateTime.Now.AddDays(-10),
                Modelo = EModeloVeiculo.SUV,
                Placa = "RIO2A55",
                Status = EStatusVeiculo.ALUGADO
            };
            var response = await Client.PostAsJsonAsync("/api/veiculo", request);
            var respostaEsperada = new List<string>
            {
                "Data não pode ser inferior a data atual.",
            };
            ValidarMensagemErro(respostaEsperada, response);
        }

        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá Retornar mensagem de erro status não permitido.")]
        public async Task DeveraRetornarMensagemErroStatusNaoPermitido()
        {
            var request = new CriarVeiculoComando
            {
                DataCadastro = DateTime.Now.AddDays(10),
                Modelo = EModeloVeiculo.SUV,
                Placa = "RIO2A55",
                Status = (EStatusVeiculo)5
            };
            var response = await Client.PostAsJsonAsync("/api/veiculo", request);
            var respostaEsperada = new List<string>
            {
                "Status não permitido, escolha um status válido: 1-Disponivel, 2-Alugado.",
            };
            ValidarMensagemErro(respostaEsperada, response);
        }
        #endregion

        #region Atualizar Status
        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá Atualizar status do veículo.")]
        public async Task DeveraAtualizarStatusVeiculo()
        {
            var veiculo = new VeiculoBuilder().ComStatus(EStatusVeiculo.ALUGADO)
                                              .Create();

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            var comando = new AtualizarStatusVeiculoComando
            {
                Placa = veiculo.Placa,
                Status = EStatusVeiculo.DISPONIVEL
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(comando), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri("http://localhost/api/veiculo/status"),
                Content = content
            };

            var response = await Client.SendAsync(request);
            var result = response.Content.ReadAsStringAsync().Result;
            var veiculoStatusAtualizado = JsonConvert.DeserializeObject<AtualizarStatusVeiculoComando>(result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            veiculoStatusAtualizado.Should().NotBeNull();
            veiculoStatusAtualizado.Status.Should().Be(EStatusVeiculo.DISPONIVEL);
        }

        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá retornar mensagem de erro ao Atualizar status do veículo com placa inexistente.")]
        public async Task DeveraRetornarMensagemErroAtualizarStatusVeiculoPlacaInexistente()
        {
            var veiculo = new VeiculoBuilder().ComPlaca("RIO2C59")
                                              .Create();

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            var comando = new AtualizarStatusVeiculoComando
            {
                Placa = "RIO2C11",
                Status = EStatusVeiculo.DISPONIVEL
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(comando), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri("http://localhost/api/veiculo/status"),
                Content = content
            };

            var response = await Client.SendAsync(request);
            var result = response.Content.ReadAsStringAsync().Result;
            var respostaEsperada = "Não foi possível concluir a ação.";
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            result.Should().Be(respostaEsperada);
        }

        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá retornar mensagem de erro ao Atualizar status do veículo com status inválido.")]
        public async Task DeveraRetornarMensagemErroAtualizarStatusVeiculoStatusInvalido()
        {
            var comando = new AtualizarStatusVeiculoComando
            {
                Placa = "RIO2C59",
                Status = (EStatusVeiculo)5
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(comando), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Patch,
                RequestUri = new Uri("http://localhost/api/veiculo/status"),
                Content = content
            };

            var response = await Client.SendAsync(request);
            var respostaEsperada = new List<string>
            {
                "Não foi possível atualizar a placa para o status informado.",
            };

            ValidarMensagemErro(respostaEsperada, response);
        }
        #endregion

        #region ListarPorModelo
        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Consulta - Deverá listar veículos por modelo.")]
        public async Task DeveraListarVeiculosPorModelo()
        {
            var veiculosEsperados = new VeiculoBuilder()
                                        .ComModelo(EModeloVeiculo.SEDAN)
                                        .CreateMany(5);

            var veiculosNaoEsperados = new VeiculoBuilder()
                                        .ComModelo(EModeloVeiculo.SUV)
                                        .CreateMany(5);
            veiculosEsperados.Concat(veiculosNaoEsperados);

            _context.Veiculos.AddRange(veiculosEsperados);
            await _context.SaveChangesAsync();

            var response = await Client.GetAsync($@"/api/veiculo/modelo/{EModeloVeiculo.SEDAN}");
            var result = response.Content.ReadAsStringAsync().Result;
            var veiculosPorModelo = JsonConvert.DeserializeObject<IList<VeiculoViewModel>>(result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            veiculosPorModelo.Should().NotBeNull();
            veiculosPorModelo.Should().OnlyContain(p => p.Modelo == EModeloVeiculo.SEDAN);
        }
        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Consulta - Deverá retornar no content ao listar veículos por modelo.")]
        public async Task DeveraRetornarNoContentListarVeiculosPorModelo()
        {
            var veiculosEsperados = new VeiculoBuilder()
                                        .ComModelo(EModeloVeiculo.SEDAN)
                                        .CreateMany(5);

            _context.Veiculos.AddRange(veiculosEsperados);
            await _context.SaveChangesAsync();

            var response = await Client.GetAsync($@"/api/veiculo/modelo/{EModeloVeiculo.SUV}");
            var result = response.Content.ReadAsStringAsync().Result;

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        #endregion

        #region BuscarPorPlaca
        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Consulta - Deverá buscar veículo por placa.")]
        public async Task DeveraBuscarVeiculosPorPlaca()
        {
            var veiculo = new VeiculoBuilder()
                                        .ComPlaca("RIO2A44")
                                        .Create();

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            var response = await Client.GetAsync($@"/api/veiculo/placa/{veiculo.Placa}");
            var result = response.Content.ReadAsStringAsync().Result;
            var vaiculoPorPlaca = JsonConvert.DeserializeObject<VeiculoViewModel>(result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            vaiculoPorPlaca.Should().NotBeNull();
            vaiculoPorPlaca.Placa.Should().Be(veiculo.Placa);
        }

        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Consulta - Deverá retornar mensagem de erro ao buscar veículo por placa inexistente.")]
        public async Task DeveraRetornarMensagemErroBuscarVeiculosPorPlaca()
        {
            var veiculo = new VeiculoBuilder()
                                        .ComPlaca("RIO2A44")
                                        .Create();

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            var response = await Client.GetAsync($@"/api/veiculo/placa/{"RIO2A45"}");
            var result = response.Content.ReadAsStringAsync().Result;
            var respostaEsperada = "Não foi possível concluir a ação.";
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            result.Should().Be(respostaEsperada);
        }
        #endregion

        #region ListarPorStatus
        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Consulta - Deverá listar veículos por status.")]
        public async Task DeveraListarVeiculosPorStatus()
        {
            var veiculosDisponiveis = new VeiculoBuilder()
                                        .ComStatus(EStatusVeiculo.DISPONIVEL)
                                        .CreateMany(5);

            var veiculosAlugados = new VeiculoBuilder()
                                       .ComStatus(EStatusVeiculo.ALUGADO)
                                       .CreateMany(5);

            veiculosDisponiveis.Concat(veiculosAlugados);

            _context.Veiculos.AddRange(veiculosDisponiveis);
            await _context.SaveChangesAsync();

            var response = await Client.GetAsync($@"/api/veiculo/status/{EStatusVeiculo.DISPONIVEL}");
            var result = response.Content.ReadAsStringAsync().Result;
            var vaiculoPorPlaca = JsonConvert.DeserializeObject<IList<VeiculoViewModel>>(result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            vaiculoPorPlaca.Should().NotBeNull();
            veiculosDisponiveis.Should().OnlyContain(p => p.Status == EStatusVeiculo.DISPONIVEL);
        }

        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Consulta - Deverá retornar mensagem de erro ao buscar veículos por status inexistente.")]
        public async Task DeveraRetornarMensagemErroBuscarVeiculosPorStatus()
        {
            var veiculo = new VeiculoBuilder()
                                        .ComPlaca("RIO2A99")
                                        .ComModelo(EModeloVeiculo.SEDAN)
                                        .ComStatus(EStatusVeiculo.DISPONIVEL)
                                        .ComDataCadastro(DateTime.Now)
                                        .CreateMany(5);

            _context.Veiculos.AddRange(veiculo);
            await _context.SaveChangesAsync();

            var response = await Client.GetAsync($@"/api/veiculo/status/{EStatusVeiculo.ALUGADO}");
            var result = response.Content.ReadAsStringAsync().Result;
            var respostaEsperada = "Não foi possível concluir a ação.";
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            result.Should().Be(respostaEsperada);
        }
        #endregion

        #region Remover
        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá remover um veículo.")]
        public async Task DeveraRemoverUmVeiculo()
        {
            var veiculo = new VeiculoBuilder()
                                        .ComStatus(EStatusVeiculo.DISPONIVEL)
                                        .ComDataCadastro(DateTime.Now.AddDays(-20))
                                        .Create();

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            var response = await Client.DeleteAsync($@"/api/veiculo/remover/{veiculo.Id}");
            var result = response.Content.ReadAsStringAsync().Result;
            var respostaValidacao = JsonConvert.DeserializeObject<RespostaException>(result);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá retornar mensagem de erro ao remover um veículo alugado.")]
        public async Task DeveraRetornarMensagemErroRemoverUmVeiculoAlugado()
        {
            var veiculo = new VeiculoBuilder()
                                        .ComStatus(EStatusVeiculo.ALUGADO)
                                        .ComDataCadastro(DateTime.Now.AddDays(20))
                                        .Create();

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            var response = await Client.DeleteAsync($@"/api/veiculo/remover/{veiculo.Id}");

            var respostaEsperada = new List<string>
            {
                "Não foi possível continuar com a remoção deste veículo.",
            };
            ValidarMensagemErro(respostaEsperada, response);
        }

        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Comando - Deverá retornar mensagem de erro ao remover um veículo com menos de 15 dias.")]
        public async Task DeveraRetornarMensagemErroRemoverUmVeiculoMenosQuinzeDias()
        {
            var veiculo = new VeiculoBuilder()
                                        .ComStatus(EStatusVeiculo.DISPONIVEL)
                                        .ComDataCadastro(DateTime.Now)
                                        .Create();

            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();

            var response = await Client.DeleteAsync($@"/api/veiculo/remover/{veiculo.Id}");

            var respostaEsperada = new List<string>
            {
                "Não foi possível continuar com a remoção deste veículo.",
            };
            ValidarMensagemErro(respostaEsperada, response);
        }
        #endregion

        #region ListarEventos
        [Trait("Integracao", "Veículo")]
        [Fact(DisplayName = "Evento - Deverá listar eventos do veículos por placa.")]
        public async Task DeveraListarEventosVeiculosPorPlaca()
        {
            var placaEventos = "RIO5A88";
            var eventosEsperados = new List<string>
            {
               $@"Veículo {placaEventos} foi salvo dia {DateTime.Now:dd//MM//yyyy}",
               $@"Veículo {placaEventos} foi locado dia {DateTime.Now:dd//MM//yyyy}",
               $@"Veículo {placaEventos} foi devolvido dia {DateTime.Now:dd//MM//yyyy}",
            };
            var eventoSalvo = new VeiculoEventoBuilder()
                                    .ComPlaca(placaEventos)
                                    .ComAcao(EAcaoVeiculoEvento.SALVO)
                                    .Create();

            var eventoLocado = new VeiculoEventoBuilder()
                                        .ComPlaca(placaEventos)
                                        .ComAcao(EAcaoVeiculoEvento.LOCADO)
                                        .Create();

            var eventoDevolvido = new VeiculoEventoBuilder()
                                        .ComPlaca(placaEventos)
                                        .ComAcao(EAcaoVeiculoEvento.DEVOLVIDO)
                                        .Create();

            var listaEventos = new List<VeiculoEvento> { eventoSalvo, eventoLocado, eventoDevolvido };
            _context.VeiculoEventos.AddRange(listaEventos);
            await _context.SaveChangesAsync();

            var response = await Client.GetAsync($@"/api/veiculo/listarEventos/{eventoSalvo.PlacaVeiculo}");
            var result = response.Content.ReadAsStringAsync().Result;
            var eventosPorPlaca = JsonConvert.DeserializeObject<IList<string>>(result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            eventosPorPlaca.Should().NotBeNull();
            eventosPorPlaca.Count.Should().Be(3);
            eventosPorPlaca.Should().BeEquivalentTo(eventosEsperados);
        }
        #endregion
    }
}

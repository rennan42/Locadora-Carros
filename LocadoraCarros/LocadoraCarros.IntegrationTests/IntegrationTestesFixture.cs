using FluentAssertions;
using LocadoraCarros.Api.Configuracao;
using LocadoraCarros.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace LocadoraCarros.IntegrationTests
{
    public class IntegrationTestesFixture : IDisposable
    {
        public readonly LocadoraCarrosApplicationFactory _factory;
        public HttpClient Client;

        protected readonly Contexto _context;
        public IntegrationTestesFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions()
            {
                HandleCookies = false,
                BaseAddress = new Uri("http://localhost"),
                AllowAutoRedirect = true,
                MaxAutomaticRedirections = 7
            };
            var options = new DbContextOptionsBuilder<Contexto>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LocadoraTestes;Trusted_Connection=True;MultipleActiveResultSets=true")
            .Options;
            _context = new Contexto(options);
            _factory = new LocadoraCarrosApplicationFactory();
            Client = _factory.CreateClient(clientOptions);
            _context.Veiculos.RemoveRange(_context.Veiculos);
            _context.VeiculoEventos.RemoveRange(_context.VeiculoEventos);
        }

        protected static void ValidarMensagemErro(List<string> respostaEsperada, HttpResponseMessage response)
        {
            var result = response.Content.ReadAsStringAsync().Result;
            var respostaValidacao = JsonConvert.DeserializeObject<RespostaException>(result);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            respostaValidacao.Errors.Select(p => p.Mensagem).Should().Equal(respostaEsperada);
        }
        public void Dispose()
        {
            Client.Dispose();
            _factory.Dispose();
        }
    }
}

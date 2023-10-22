using LocadoraCarros.Domain.Entidades;

namespace LocadoraCarros.Domain.Repositorios
{
    public interface IRepositorio<TEntity> : IDisposable where TEntity : Entidade
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(long id);
        Task Remover(long id);
        Task<int> SaveChanges();
    }
}

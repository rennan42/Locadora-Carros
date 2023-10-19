using LocadoraCarros.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LocadoraCarros.Infrastructure.Repositorios
{
    public interface IRepositorio<TEntity> : IDisposable where TEntity : Entidade
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(long id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(long id);
        Task<int> SaveChanges();
    }
    public abstract class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : Entidade, new()
    {
        protected readonly Contexto Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repositorio(Contexto db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }


        public async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task<TEntity> ObterPorId(long id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public async Task Remover(long id)
        {
            Db.Remove(new TEntity
            {
                Id = id
            });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}

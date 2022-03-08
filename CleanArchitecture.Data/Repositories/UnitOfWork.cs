using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly StreamerDBContext _context;
        private IVideoRepository _videoRepository;
        private IStreamerRepository _streamerRepository;

        public IVideoRepository VideoRepository => _videoRepository ??= new VideoRepository(_context);
        public IStreamerRepository StreamerRepository => _streamerRepository ??= new StreamerRepository(_context);

       

        public UnitOfWork(StreamerDBContext context)
        {
            _context = context;
         
        }

        public StreamerDBContext StreamerDBContext => _context;

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
           _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if(_repositories == null)
                _repositories = new Hashtable();

            //COMPROBAMOS SI EXISTE EL TEntity instancia de repositorio, si no existe lo agregamos
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            { 
               var respositoryType = typeof(RepositoryBase<>);
                _repositories.Add(type, respositoryType);

                var respositoryInstance = Activator.CreateInstance(respositoryType.MakeGenericType(typeof(TEntity)),_context);
                
                _repositories.Add(type,respositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}

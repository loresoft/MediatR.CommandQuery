using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using MediatR.CommandQuery.Commands;
using MediatR.CommandQuery.Definitions;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

using MongoDB.Abstracts;

namespace MediatR.CommandQuery.MongoDB.Handlers
{
    public class EntityPatchCommandHandler<TRepository, TEntity, TKey, TReadModel>
        : RepositoryHandlerBase<TRepository, TEntity, TKey, EntityPatchCommand<TKey, TReadModel>, TReadModel>
        where TRepository : IMongoRepository<TEntity, TKey>
        where TEntity : class, IHaveIdentifier<TKey>, new()
    {
        public EntityPatchCommandHandler(ILoggerFactory loggerFactory, TRepository repository, IMapper mapper)
            : base(loggerFactory, repository, mapper)
        {
        }

        protected override async Task<TReadModel> Process(EntityPatchCommand<TKey, TReadModel> request, CancellationToken cancellationToken)
        {
            var entity = await Repository
                .FindAsync(request.Id, cancellationToken)
                .ConfigureAwait(false);

            if (entity == null)
                return default;

            // apply json patch to entity
            var jsonPatch = new JsonPatchDocument(
                request.Patch.GetOperations().ToList(),
                request.Patch.ContractResolver);

            jsonPatch.ApplyTo(entity);

            // apply update metadata
            if (entity is ITrackUpdated updateEntity)
            {
                updateEntity.Updated = request.Activated;
                updateEntity.UpdatedBy = request.ActivatedBy;
            }

            var savedEntity = await Repository
                .UpdateAsync(entity, cancellationToken)
                .ConfigureAwait(false);

            // return read model
            var model = Mapper.Map<TReadModel>(savedEntity);
            return model;
        }
    }
}

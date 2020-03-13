using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MediatR.CommandQuery.Mvc
{
    public abstract class EntityQueryControllerBase<TKey, TReadModel> : MediatorControllerBase
    {
        protected EntityQueryControllerBase(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TReadModel>> Get(CancellationToken cancellationToken, TKey id)
        {
            var readModel = await GetQuery(id, cancellationToken);

            return Ok(readModel);
        }

        [HttpPost("query")]
        public async Task<ActionResult<EntityPagedResult<TReadModel>>> Query(CancellationToken cancellationToken, EntityQuery query)
        {
            var listResult = await PagedQuery(query, cancellationToken);

            return Ok(listResult);
        }

        [HttpGet("")]
        public async Task<ActionResult<EntityPagedResult<TReadModel>>> Query(CancellationToken cancellationToken, string q = null, string sort = null, int page = 1, int size = 20)
        {
            var query = new EntityQuery(q, page, size, sort);
            var listResult = await PagedQuery(query, cancellationToken);

            return Ok(listResult);
        }
        

        protected virtual async Task<TReadModel> GetQuery(TKey id, CancellationToken cancellationToken = default)
        {
            var command = new EntityIdentifierQuery<TKey, TReadModel>(User, id);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        protected virtual async Task<EntityPagedResult<TReadModel>> PagedQuery(EntityQuery entityQuery, CancellationToken cancellationToken = default)
        {
            var command = new EntityPagedQuery<TReadModel>(User, entityQuery);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        protected virtual async Task<IReadOnlyCollection<TReadModel>> SelectQuery(EntityFilter entityFilter, EntitySort entitySort, CancellationToken cancellationToken = default)
        {
            var command = new EntitySelectQuery<TReadModel>(User, entityFilter, entitySort);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }
    }
}
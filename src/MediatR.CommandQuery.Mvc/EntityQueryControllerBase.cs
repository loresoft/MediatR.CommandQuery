using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MediatR.CommandQuery.Mvc
{
    public abstract class EntityQueryControllerBase<TKey, TQueryModel, TReadModel> : MediatorControllerBase
    {
        protected EntityQueryControllerBase(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TReadModel>> Get(CancellationToken cancellationToken, TKey id)
        {
            var readModel = await GetQuery(id, cancellationToken);

            return Ok(readModel);
        }

        [HttpPost("page")]
        public virtual async Task<ActionResult<EntityPagedResult<TQueryModel>>> Page(CancellationToken cancellationToken, EntityQuery query)
        {
            var listResult = await PagedQuery(query, cancellationToken);

            return Ok(listResult);
        }

        [HttpGet("page")]
        public virtual async Task<ActionResult<EntityPagedResult<TQueryModel>>> Page(CancellationToken cancellationToken, string q = null, string sort = null, int page = 1, int size = 20)
        {
            var query = new EntityQuery(q, page, size, sort);
            var listResult = await PagedQuery(query, cancellationToken);

            return Ok(listResult);
        }
        
        [HttpPost("query")]
        public virtual async Task<ActionResult<IReadOnlyCollection<TQueryModel>>> Query(CancellationToken cancellationToken, EntitySelect query)
        {
            var results = await SelectQuery(query, cancellationToken);

            return Ok(results);
        }

        [HttpGet("")]
        public virtual async Task<ActionResult<IReadOnlyCollection<TQueryModel>>> Query(CancellationToken cancellationToken, string q = null, string sort = null)
        {
            var query = new EntitySelect(q, sort);
            var results = await SelectQuery(query, cancellationToken);

            return Ok(results);
        }


        protected virtual async Task<TReadModel> GetQuery(TKey id, CancellationToken cancellationToken = default)
        {
            var command = new EntityIdentifierQuery<TKey, TReadModel>(User, id);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        protected virtual async Task<EntityPagedResult<TQueryModel>> PagedQuery(EntityQuery entityQuery, CancellationToken cancellationToken = default)
        {
            var command = new EntityPagedQuery<TQueryModel>(User, entityQuery);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        protected virtual async Task<IReadOnlyCollection<TQueryModel>> SelectQuery(EntitySelect entitySelect, CancellationToken cancellationToken = default)
        {
            var command = new EntitySelectQuery<TQueryModel>(User, entitySelect);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }
    }
}
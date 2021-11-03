using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

using MediatR.CommandQuery.Queries;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediatR.CommandQuery.Mvc
{
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class EntityQueryControllerBase<TKey, TListModel, TReadModel> : MediatorControllerBase
    {
        protected EntityQueryControllerBase(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public virtual async Task<ActionResult<TReadModel>> Get(CancellationToken cancellationToken, [FromRoute] TKey id)
        {
            var readModel = await GetQuery(id, cancellationToken);

            return readModel;
        }

        [HttpPost("page")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public virtual async Task<ActionResult<EntityPagedResult<TListModel>>> Page(CancellationToken cancellationToken, [FromBody] EntityQuery query)
        {
            var listResult = await PagedQuery(query, cancellationToken);

            return listResult;
        }

        [HttpGet("page")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public virtual async Task<ActionResult<EntityPagedResult<TListModel>>> Page(CancellationToken cancellationToken, [FromQuery] string q = null, [FromQuery] string sort = null, [FromQuery] int page = 1, [FromQuery] int size = 20)
        {
            var query = new EntityQuery(q, page, size, sort);
            var listResult = await PagedQuery(query, cancellationToken);

            return listResult;
        }

        [HttpPost("query")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public virtual async Task<ActionResult<IReadOnlyCollection<TListModel>>> Query(CancellationToken cancellationToken, [FromBody] EntitySelect query)
        {
            var results = await SelectQuery(query, cancellationToken);

            return results.ToList();
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public virtual async Task<ActionResult<IReadOnlyCollection<TListModel>>> Query(CancellationToken cancellationToken, [FromQuery] string q = null, [FromQuery] string sort = null)
        {
            var query = new EntitySelect(q, sort);
            var results = await SelectQuery(query, cancellationToken);

            return results.ToList();
        }


        protected virtual async Task<TReadModel> GetQuery(TKey id, CancellationToken cancellationToken = default)
        {
            var command = new EntityIdentifierQuery<TKey, TReadModel>(User, id);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        protected virtual async Task<EntityPagedResult<TListModel>> PagedQuery(EntityQuery entityQuery, CancellationToken cancellationToken = default)
        {
            var command = new EntityPagedQuery<TListModel>(User, entityQuery);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        protected virtual async Task<IReadOnlyCollection<TListModel>> SelectQuery(EntitySelect entitySelect, CancellationToken cancellationToken = default)
        {
            var command = new EntitySelectQuery<TListModel>(User, entitySelect);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }
    }
}

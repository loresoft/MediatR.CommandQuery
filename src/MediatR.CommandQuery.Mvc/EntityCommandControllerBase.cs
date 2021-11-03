using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Commands;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MediatR.CommandQuery.Mvc
{
    [Produces(MediaTypeNames.Application.Json)]
    public abstract class EntityCommandControllerBase<TKey, TListModel, TReadModel, TCreateModel, TUpdateModel>
        : EntityQueryControllerBase<TKey, TListModel, TReadModel>
    {
        protected EntityCommandControllerBase(IMediator mediator) : base(mediator)
        {
        }


        [HttpPost("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public virtual async Task<ActionResult<TReadModel>> Create(CancellationToken cancellationToken, [FromBody] TCreateModel createModel)
        {
            var readModel = await CreateCommand(createModel, cancellationToken);

            return readModel;
        }

        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public virtual async Task<ActionResult<TReadModel>> Update(CancellationToken cancellationToken, [FromRoute] TKey id, [FromBody] TUpdateModel updateModel)
        {
            var readModel = await UpdateCommand(id, updateModel, cancellationToken);

            return readModel;
        }

        [HttpPatch("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public virtual async Task<ActionResult<TReadModel>> Patch(CancellationToken cancellationToken, [FromRoute] TKey id, [FromBody] JsonPatchDocument jsonPatch)
        {
            var readModel = await PatchCommand(id, jsonPatch, cancellationToken);

            return readModel;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public virtual async Task<ActionResult<TReadModel>> Delete(CancellationToken cancellationToken, [FromRoute] TKey id)
        {
            var readModel = await DeleteCommand(id, cancellationToken);

            return readModel;
        }


        protected virtual async Task<TReadModel> CreateCommand(TCreateModel createModel, CancellationToken cancellationToken = default)
        {
            var command = new EntityCreateCommand<TCreateModel, TReadModel>(User, createModel);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        protected virtual async Task<TReadModel> UpdateCommand(TKey id, TUpdateModel updateModel, CancellationToken cancellationToken = default)
        {
            var command = new EntityUpdateCommand<TKey, TUpdateModel, TReadModel>(User, id, updateModel);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        protected virtual async Task<TReadModel> UpsertCommand(TKey id, TUpdateModel updateModel, CancellationToken cancellationToken = default)
        {
            var command = new EntityUpsertCommand<TKey, TUpdateModel, TReadModel>(User, id, updateModel);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        protected virtual async Task<TReadModel> PatchCommand(TKey id, IJsonPatchDocument jsonPatch, CancellationToken cancellationToken = default)
        {
            var command = new EntityPatchCommand<TKey, TReadModel>(User, id, jsonPatch);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }

        protected virtual async Task<TReadModel> DeleteCommand(TKey id, CancellationToken cancellationToken = default)
        {
            var command = new EntityDeleteCommand<TKey, TReadModel>(User, id);
            var result = await Mediator.Send(command, cancellationToken);

            return result;
        }
    }
}

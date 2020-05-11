using System.Threading;
using System.Threading.Tasks;
using MediatR.CommandQuery.Commands;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MediatR.CommandQuery.Mvc
{
    public abstract class EntityCommandControllerBase<TKey, TReadModel, TCreateModel, TUpdateModel>
        : EntityQueryControllerBase<TKey, TReadModel>
    {
        protected EntityCommandControllerBase(IMediator mediator) : base(mediator)
        {
        }


        [HttpPost("")]
        public virtual async Task<ActionResult<TReadModel>> Create(CancellationToken cancellationToken, TCreateModel createModel)
        {
            var readModel = await CreateCommand(createModel, cancellationToken);

            return Ok(readModel);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TReadModel>> Update(CancellationToken cancellationToken, TKey id, TUpdateModel updateModel)
        {
            var readModel = await UpdateCommand(id, updateModel, cancellationToken);

            return Ok(readModel);
        }

        [HttpPatch("{id}")]
        public virtual async Task<ActionResult<TReadModel>> Patch(CancellationToken cancellationToken, TKey id, JsonPatchDocument jsonPatch)
        {
            var readModel = await PatchCommand(id, jsonPatch, cancellationToken);

            return Ok(readModel);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TReadModel>> Delete(CancellationToken cancellationToken, TKey id)
        {
            var readModel = await DeleteCommand(id, cancellationToken);

            return Ok(readModel);
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
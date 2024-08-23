using MediatR;
using MediatR.CommandQuery.Models;

namespace Tracker.WebService.Domain.Commands;

public class BackgroundUpdateCommand : IRequest<CompleteModel>
{

    public BackgroundUpdateCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }


    public override string ToString()
    {
        return $"Background Update {Id}";
    }
}

namespace MediatR.CommandQuery.Dispatcher;

public class DispatcherOptions
{
    public string RoutePrefix { get; set; } = "/api";

    public string SendRoute { get; set; } = "/dispatcher";

}

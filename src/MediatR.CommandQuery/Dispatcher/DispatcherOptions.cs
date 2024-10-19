namespace MediatR.CommandQuery.Dispatcher;

public class DispatcherOptions
{
    public string RoutePrefix { get; set; } = "/dispatcher";

    public string SendRoute { get; set; } = "/send";

}

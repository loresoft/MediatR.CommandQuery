namespace MediatR.CommandQuery.Dispatcher;

public class DispatcherOptions
{
    public string FeaturePrefix { get; set; } = "/api";

    public string DispatcherPrefix { get; set; } = "/dispatcher";

    public string SendRoute { get; set; } = "/send";
}

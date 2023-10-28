namespace Adventure.Net;

internal class ExecuteResult
{
    public ExecuteResult(bool success, CommandOutput commandOutput)
    {
        Success = success;
        CommandOutput = commandOutput;
    }

    public bool Success { get; }
    public CommandOutput CommandOutput { get; }
}

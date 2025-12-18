namespace Core.TraceIdLogic.Interfaces;

public interface ITraceWriter
{
    string Name { get; }

    string GetValue();
}
namespace Core.TraceIdLogic.Interfaces;

public interface ITraceReader
{
    string Name { get; }

    void WriteValue(string value);
}
using System.Collections.Generic;

public interface IDebuggable
{
    Dictionary<string, object> GetDebugData();
}
namespace RedisAssistant.Models;

public class RedisServerInfo
{
    public string Version { get; set; } = string.Empty;
    public long UsedMemory { get; set; }
    public long TotalKeys { get; set; }
    public int ConnectedClients { get; set; }
    public long TotalCommandsProcessed { get; set; }
    public double Uptime { get; set; }
}

namespace RedisAssistant.Models;

public class RedisKey
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = "string";
    public long Size { get; set; }
    public TimeSpan? Ttl { get; set; }
}

namespace Logik.MultiAiCoder.Engine
{
    /// <summary>
    /// Represents a normalized response coming from any AI provider.
    /// </summary>
    public class ResultModel
    {
        public string Provider { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}

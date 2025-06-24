using System;

namespace Logik.MultiAICoder.Engine
{
    public class PromptConfiguration
    {
        public string Provider { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}

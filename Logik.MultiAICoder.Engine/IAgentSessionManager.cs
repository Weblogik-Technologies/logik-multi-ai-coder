using System.Collections.Generic;

namespace Logik.MultiAiCoder.Engine
{
    /// <summary>
    /// Manages mapping between a user/project context and AI agent identifiers.
    /// </summary>
    public interface IAgentSessionManager
    {
        /// <summary>
        /// Gets an identifier of the agent for the provided context.
        /// </summary>
        string GetAgentId(string provider, IDictionary<string, string> context);

        /// <summary>
        /// Sets the agent identifier for the given context.
        /// </summary>
        void SetAgentId(string provider, IDictionary<string, string> context, string agentId);
    }
}

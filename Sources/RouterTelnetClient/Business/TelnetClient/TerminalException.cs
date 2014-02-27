using System;

namespace RouterTelnetClient.TelnetClient
{
    /// <summary>
    /// Exception dealing with parsing ...
    /// </summary>
    public class TerminalException : ApplicationException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception's message</param>
        public TerminalException(string message)
            : base(message)
        {
            // further code
        }
    }
}
using System;

namespace RouterTelnetClient.TelnetClient
{
    /// <summary>
    /// Exception dealing with connectivity
    /// </summary>
    public class TelnetException : ApplicationException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception's message</param>
        public TelnetException(string message)
            : base(message)
        {
            // further code
        }
    }
}
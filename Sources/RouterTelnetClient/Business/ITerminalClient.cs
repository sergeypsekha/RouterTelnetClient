namespace RouterTelnetClient.Business
{
    /// <summary>
    /// The Terminal interface.
    /// </summary>
    public interface ITerminalClient
    {
        /// <summary>
        /// Connect to the telnet server
        /// </summary>
        /// <returns>true if connection was successful</returns>
        bool Connect();

        /// <summary>
        /// Send a response to the server
        /// </summary>
        /// <param name="response">response String</param>
        /// <param name="endLine">terminate with appropriate end-of-line chars</param>
        /// <returns>true if sending was OK</returns>
        bool SendResponse(string response, bool endLine);

        /// <summary>
        /// Wait for a particular string
        /// </summary>
        /// <param name="searchFor">string to be found</param>
        /// <returns>string found or null if not found</returns>
        string WaitForString(string searchFor);
    }
}
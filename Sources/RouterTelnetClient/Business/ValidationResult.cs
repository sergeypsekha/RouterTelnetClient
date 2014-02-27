namespace RouterTelnetClient.Business
{
    /// <summary>
    /// The validation result.
    /// </summary>
    public class ValidationResult
    {
        public string PropertyName { get; set; }

        public string Message { get; set; }

        public ValidationResult(string propertyName, string message)
        {
            this.PropertyName = propertyName;
            this.Message = message;
        }
    }
}
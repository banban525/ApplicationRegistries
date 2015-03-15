namespace ApplicationRegistries
{
    class ValidateDetail
    {
        private readonly ValidateErrorLevel _errorLevel;
        private readonly string _message;

        public ValidateDetail(ValidateErrorLevel errorLevel, string message)
        {
            _errorLevel = errorLevel;
            _message = message;
        }

        public ValidateErrorLevel ErrorLevel
        {
            get { return _errorLevel; }
        }

        public string Message
        {
            get { return _message; }
        }
    }
}
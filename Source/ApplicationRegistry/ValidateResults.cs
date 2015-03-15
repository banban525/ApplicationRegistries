using System.Collections.Generic;
using System.Linq;

namespace ApplicationRegistries
{
    class ValidateResults
    {
        private readonly IEnumerable<ValidateDetail> _details;
        public ValidateResults(IEnumerable<ValidateDetail> details)
        {
            _details = details.ToArray();
        }

        public ValidateErrorLevel ErrorLevel
        {
            get
            {
                if (_details.Any(_ => _.ErrorLevel == ValidateErrorLevel.Error))
                {
                    return ValidateErrorLevel.Error;
                }
                if (_details.Any(_ => _.ErrorLevel == ValidateErrorLevel.Warning))
                {
                    return ValidateErrorLevel.Warning;
                }
                return ValidateErrorLevel.None;
            }
        }

        public IEnumerable<ValidateDetail> Details { get { return _details;} }

        public static ValidateResults operator +(ValidateResults x, ValidateResults y)
        {
            return new ValidateResults(x._details.Concat(y._details));
        }

        public static ValidateResults operator +(ValidateResults x, ValidateDetail y)
        {
            return new ValidateResults(x._details.Concat(new[] {y}));
        }

        public static readonly ValidateResults Empty = new ValidateResults(new ValidateDetail[] { });
    }
}
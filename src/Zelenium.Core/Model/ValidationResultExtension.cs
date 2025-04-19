using System;
using System.Linq;

namespace Zelenium.Core.Model
{
    public static class ValidationResultExtensions
    {
        public static ValidationResult Merge(this ValidationResult result, ValidationResult nextResult)
        {
            if (result == null && nextResult == null)
            {
                return new ValidationResult
                {
                    Passed = true,
                    Message = string.Empty
                };
            }

            if (result == null)
            {
                return nextResult;
            }

            if (nextResult == null)
            {
                return result;
            }

            var mergedPassed = result.Passed && nextResult.Passed;

            var mergedMessage = string.Join(Environment.NewLine, new[] { result.Message, nextResult.Message }
                                            .Where(msg => !string.IsNullOrWhiteSpace(msg)));

            return new ValidationResult
            {
                Passed = mergedPassed,
                Message = mergedMessage
            };
        }
    }
}

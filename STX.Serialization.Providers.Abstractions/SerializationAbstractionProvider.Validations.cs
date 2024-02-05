// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using STX.Serialization.Providers.Abstractions.Models.Exceptions;

namespace STX.Serialization.Providers.Abstractions
{
    public partial class SerializationAbstractionProvider
    {
        public void ValidateSerializationArgs<T>(T @object)
        {
            Validate((Rule: IsInvalid(@object), Parameter: "Object"));
        }

        private static dynamic IsInvalid<T>(T @object) => new
        {
            Condition = @object is null,
            Message = "Object is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidArgumentSerializationException = new InvalidArgumentSerializationException(
                message: "Invalid serialization argument(s), please correct the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidArgumentSerializationException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidArgumentSerializationException.ThrowIfContainsErrors();
        }
    }
}

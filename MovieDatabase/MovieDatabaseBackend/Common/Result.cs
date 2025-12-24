using System.Diagnostics.CodeAnalysis;

namespace MovieDatabaseBackend.Common
{
    public class Result<T>
    {
        public T? Value { get; private init; }

        public string? Error { get; private init; }

        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool Success { get; private init; }

        public static Result<T> Ok(T value) => new() { Value = value, Success = true };
        public static Result<T> Fail(string error) => new() { Success = false, Error = error };
    }
}

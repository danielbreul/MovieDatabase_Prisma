using System.Diagnostics.CodeAnalysis;

namespace MovieDatabaseBackend.Common
{
    public enum ResultState
    {
        Success,
        NotFound,
        Referenced,
        InvalidDto,
        Failure
    }

    public class Result
    {
        public string? Error { get; protected init; }
        public ResultState State { get; protected init; }
        public virtual bool IsSucceed => State == ResultState.Success;
        public static Result Ok() => new() { State = ResultState.Success };
        public static Result Fail(ResultState state = ResultState.Failure, string? error = null) => new() { State = state, Error = error };
    }

    public class Result<T> : Result
    {
        public T? Value { get; private init; }
        [MemberNotNullWhen(true, nameof(Value))]
        public override bool IsSucceed => State == ResultState.Success;
        public static Result<T> Ok(T value) => new() { Value = value, State = ResultState.Success };
        public static new Result<T> Fail(ResultState state = ResultState.Failure, string? error = null) => new() { State = state, Error = error };
    }
}

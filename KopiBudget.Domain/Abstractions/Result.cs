using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KopiBudget.Domain.Abstractions
{
    public class Result
    {
        #region Public Constructors

        public Result(bool isSuccess, Error error, List<Error>? errors = null)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
            Errors = errors;
        }

        #endregion Public Constructors

        #region Properties

        public bool IsSuccess { get; }

        public Error Error { get; }

        public List<Error>? Errors { get; }

        #endregion Properties

        #region Public Methods

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

        public static Result<TValue> Failure<TValue>(Error error, List<Error> errors) => new(default, false, error, errors);

        public static Result<TValue> Create<TValue>(TValue? value) =>
            value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

        #endregion Public Methods
    }

    public sealed class Result<TValue> : Result
    {
        #region Fields

        private readonly TValue? _value;

        #endregion Fields

        #region Public Constructors

        public Result(TValue? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public Result(TValue? value, bool isSuccess, Error error, List<Error> errors)
            : base(isSuccess, error, errors)
        {
            _value = value;
        }

        #endregion Public Constructors

        #region Properties

        [NotNull]
        public TValue Data => _value!;

        #endregion Properties

        #region Public Methods

        public static implicit operator Result<TValue>(TValue? value) => Create(value);

        #endregion Public Methods
    }
}
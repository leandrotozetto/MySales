using MySales.Product.Api.Domain.Core.Notifications.Interfaces;

namespace MySales.Product.Api.Domain.Core.Validations.Argument
{
    /// <summary>
    /// Ensure that arguments are valids.
    /// </summary>
    public class Ensure : IEnsure
    {
        private readonly INotification _notification;

        public Ensure(INotification notification)
        {
            _notification = notification;
        }

        /// <summary>
        /// Creates a new Param <see cref="Param{T}"/>
        /// </summary>
        /// <typeparam name="T">Argument's type.</typeparam>
        /// <param name="value">Value will be check.</param>
        /// <param name="name">Argument's name.</param>
        /// <returns></returns>
        public Param<T> That<T>(T value, string name) => new Param<T>(value, name, _notification);
    }

    public interface IEnsure
    {
        /// <summary>
        /// Creates a new Param <see cref="Param{T}"/>
        /// </summary>
        /// <typeparam name="T">Argument's type.</typeparam>
        /// <param name="value">Value will be check.</param>
        /// <param name="name">Argument's name.</param>
        /// <returns></returns>
        public Param<T> That<T>(T value, string name);
    }

    public interface IEnsureResult
    {
        bool IsInvalid { get; }
    }

    public class EnsureResultSuccess : IEnsureResult
    {
        public bool IsInvalid => false;

        private EnsureResultSuccess() { }

        public static IEnsureResult New()
        {
            return new EnsureResultSuccess();
        }
    }

    public class EnsureResultError : IEnsureResult
    {
        public bool IsInvalid => true;

        private EnsureResultError() { }

        public static IEnsureResult New()
        {
            return new EnsureResultError();
        }
    }
}

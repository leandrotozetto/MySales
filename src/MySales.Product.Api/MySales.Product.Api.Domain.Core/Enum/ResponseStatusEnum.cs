using MySales.Product.Api.Domain.Core.ValuesObjects;

namespace MySales.Product.Api.Domain.Core.Enum
{
    public static class ResponseStatusEnum
    {
        public static ResponseStatus NoContent => new ResponseStatus("NoContent", true);

        public static ResponseStatus Success => new ResponseStatus("Success", true);

        public static ResponseStatus ValidationError => new ResponseStatus("ValidationError", false);

        public static ResponseStatus FatalError => new ResponseStatus("FatalError", false);

        /// <summary>
        /// Checks if status is an error.
        /// </summary>
        /// <param name="status">Status to check.</param>
        /// <returns>Return true if is error</returns>
        public static bool IsErrorStatus(ResponseStatus status)
        {
            return !status.Equals(NoContent) && !status.Equals(Success);
        }
    }
}

using MySales.Product.Api.Domain.Core.Resources;
using MySales.Product.Shared;

namespace MySales.Product.Api.Domain.Core.Validations
{
    /// <summary>
    /// Defines the validation messages.
    /// </summary>
    public static class ValidationMessage
    {
        /// <summary>
        /// For fieldNames as required. 
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <returns>Message for required field.</returns>
        public static string Required(string fieldName) =>
            string.Format(ResourcesReader.ValidationMessage(Constants.Required), fieldName);

        ///// <summary>
        ///// Defines a min and max length for the text.
        ///// </summary>
        ///// <param name="fieldName">Field name.</param>
        ///// <param name="min">Minimun length.</param>
        ///// <param name="max">Max length.</param>
        ///// <returns>Message for text length.</returns>
        public static string Length(string fieldName, int min, int max)
            => $"O campo {fieldName} deve conter no mínimo {min} e no máximo {max} caracteres.";

        /// <summary>
        /// Defines a max length for the text.
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <param name="max">Max length.</param>
        /// <returns>Message for text length.</returns>
        public static string MaxLength(string fieldName, int max) =>
            string.Format(ResourcesReader.ValidationMessage(Constants.MaxLength), fieldName, max);

        /// <summary>
        /// Defines a max length for the text.
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <param name="max">Max length.</param>
        /// <returns>Message for text length.</returns>
        public static string MinLength(string fieldName, int min)
            => $"O campo {fieldName} deve conter no mínimo {min} caracteres.";

        /// <summary>
        /// For field as numeric.
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <returns>Message for numeric fieldName.</returns>
        public static string Numeric(string fieldName) => $"O campo {fieldName} deve conter somente números.";

        /// <summary>
        /// For e-mail fieldNames.
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <returns>Message for e-mail fieldNames.</returns>
        public static string Email(string fieldName) => $"O campo {fieldName} deve conter um e-mail válido.";

        /// <summary>
        /// For password fiedls.
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <returns>Message for password fieldNames.</returns>
        public static string PasswordNumericChar(string fieldName) => $"O campo {fieldName} deve conter pelo menos um número.";

        /// <summary>
        /// For password fiedls.
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <returns>Message for password fieldNames.</returns>
        public static string PasswordUpperCaseChar(string fieldName) => $"O campo {fieldName} deve conter pelo menos uma letra maiúscula.";

        /// <summary>
        /// For password fiedls.
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <returns>Message for password fieldNames.</returns>
        public static string PasswordSpecialChar(string fieldName) => $"O campo {fieldName} deve conter pelo menos um caracter especial.";

        /// <summary>
        /// For password fiedls.
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <param name="value">Value that will be comparabled.</param>
        /// <returns>Message for text fieldNames.</returns>
        public static string GreaterThan(string fieldName, dynamic value) => $"O campo {fieldName} deve conter o valor maior que {value}.";

        /// <summary>
        /// For field as date.
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <returns>Message for date fieldName.</returns>
        public static string Date(string fieldName) => $"O campo {fieldName} deve conter uma data válida.";

        /// <summary>
        /// For field as object.
        /// </summary>
        /// <param name="fieldName">Field name.</param>
        /// <returns>Message for null object fieldName.</returns>
        public static string NotNull(string fieldName) => $"O campo {fieldName} não pode ser nulo.";
    }
}

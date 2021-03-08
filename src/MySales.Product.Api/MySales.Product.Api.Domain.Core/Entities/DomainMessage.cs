using MySales.Product.Api.Domain.Core.Resources;

namespace MySales.Product.Api.Domain.Core.Entities
{
    /// <summary>
    /// Contains standardized messages
    /// </summary>
    public static class DomainMessage
    {
        /// <summary>
        /// Message to inform that login or password is invalid.
        /// </summary>
        public static string LoginPasswordInvalid => ResourcesReader.Message(nameof(LoginPasswordInvalid));

        /// <summary>
        /// Message to inform that entity can't be removed, because it has ralationship another entities
        /// </summary>
        /// <param name="mainEntity"></param>
        /// <param name="entityRelated"></param>
        /// <returns></returns>
        public static string EntityHasRelationship(string mainEntity, string[] entityRelated)
        {
            var entitiesRelated = string.Join(',', entityRelated);

            return $"{ResourcesReader.Message(nameof(EntityHasRelationship))}";
        }

        /// <summary>
        /// Message to inform that id not exist.
        /// </summary>
        /// <param name="id">Value of the id.</param>
        /// <returns>Returns error message.</returns>
        public static string IdNotExist(dynamic id) => $"O id {id} informado não existe.";

        /// <summary>
        /// Message to inform that entity is null.
        /// </summary>
        /// <param name="entity">Name of the entity.</param>
        /// <returns>Returns error message.</returns>
        public static string EntityIsNull(dynamic entity) => $"Os dados da entidade {entity} não existem.";

        /// <summary>
        /// Message to inform that argument is null.
        /// </summary>
        /// <param name="entity">Name of the entity.</param>
        /// <returns>Returns error message.</returns>
        public static string ArgumentIsNull(string name) => $"O argumento {name} é inválido (null).";

        /// <summary>
        /// Message to inform that value is null, empty or whitespace.
        /// </summary>
        /// <param name="name">Name of the argument.</param>
        /// <returns>Returns error message.</returns>
        public static string IsNullOrWhiteSpace(string name) => $"O argumento {name} não pode ser nulo, vazio ou conter somente espaços em branco.";
    }
}
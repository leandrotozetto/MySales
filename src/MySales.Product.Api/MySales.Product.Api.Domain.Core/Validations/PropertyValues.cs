namespace MySales.Product.Api.Domain.Core.Validations
{
    public struct PropertyValues
    {
        public string FieldName { get; private set; }

        public dynamic FieldValue { get; private set; }

        public PropertyValues(string fieldName, dynamic fielValue)
        {
            FieldName = fieldName;
            FieldValue = fielValue;
        }
    }
}

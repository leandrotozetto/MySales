namespace MySales.Product.Api.Domain.Core.ValuesObjects
{
    public struct ResponseStatus
    {
        public string Name { get; }

        public bool Value { get; }

        public ResponseStatus(string name, bool value)
        {
            Name = name;
            Value = value;
        }
    }
}

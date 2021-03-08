namespace MySales.Product.Api.Domain.Core.ValuesObjects
{
    public struct Status
    {
        public string Name { get; }

        public bool Value { get; }

        public Status(string name, bool value)
        {
            Name = name;
            Value = value;
        }
    }
}

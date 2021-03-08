namespace MySales.Product.Api.Domain.Core.Enum
{
    public class NotificationTypeEnum
    {
        public static NotificationType Error => new NotificationType("Error", 500);

        public static NotificationType Information => new NotificationType("BadRequest", 400);
    }

    public class NotificationType
    {
        public string Name { get; }

        public int Value { get; }

        public NotificationType(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}

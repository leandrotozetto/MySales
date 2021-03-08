using MySales.Product.Api.Domain.Core.ValuesObjects;
using System.Diagnostics.Tracing;

namespace MySales.Product.Api.Domain.Core.Enum
{
    public class StatusEnum
    {
        public static Status Enable => new Status("Active", true);

        public static Status Disable => new Status("Disable", false);

        public static Status New(bool status)
        {
            return status ? Enable : Disable;
        }
    }
}

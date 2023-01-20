using System.ComponentModel;

namespace API.Enums
{
    public enum OrderState
    {
        [Description("Utworzone")]
        Created = 0,
        [Description("Anulowane")]
        Cancelled = 1,
        [Description("W realizacji")]
        InRealization = 2,
        [Description("Wysłane")]
        Sent = 3,
        [Description("Zrealizowane")]
        Realized = 4
    }
}

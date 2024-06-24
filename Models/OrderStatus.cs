using System.Runtime.Serialization;

namespace E_commercial_Web_RESTAPI.Models
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Payment Failed")]
        Failed,

        [EnumMember(Value = "Payment Succeeded")]
        PaymentSucceeded,

        
    }
}

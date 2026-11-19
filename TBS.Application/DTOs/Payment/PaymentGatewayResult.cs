using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Enums;

namespace TBS.Application.DTOs.Payment
{
    public record PaymentGatewayResult
    (
        bool IsSuccess,
        string Provider,
        string ProviderTransactionId,
        PaymentStatus InitialStatus,
        string? ErrorMessage=null

        );
    
}

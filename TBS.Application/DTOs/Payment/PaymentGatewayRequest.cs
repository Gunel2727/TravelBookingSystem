using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Domain.Enums;

namespace TBS.Application.DTOs.Payment
{
    public record PaymentGatewayRequest
   (
        PaymentMethod PaymentMethod,
        Currency Currency,
        decimal Amount,
        string ReferenceId
        );
}

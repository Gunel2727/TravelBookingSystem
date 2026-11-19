using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Payment;

namespace TBS.Application.Interfaces
{
    public interface IPaymentGatewayService
    {
        Task<PaymentGatewayResult> CreatePaymentAsync(PaymentGatewayRequest request,CancellationToken cancellationToken = default);
    }
}

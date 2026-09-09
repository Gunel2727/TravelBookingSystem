using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBS.Application.DTOs.Payment;

namespace TBS.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IList<PaymentDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PaymentDto> AddAsync(CreatePaymentDto dto, CancellationToken cancellationToken = default);
        Task<PaymentDto> UpdateAsync(Guid id, UpdatePaymentDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    }
}

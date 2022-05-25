using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Orders.Queries.GetOrdersList.ViewModels;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderVM>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<List<OrderVM>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repository.GetByUserNameAsync(request.UserName);
            return _mapper.Map<List<OrderVM>>(orders);
        }
    }
}

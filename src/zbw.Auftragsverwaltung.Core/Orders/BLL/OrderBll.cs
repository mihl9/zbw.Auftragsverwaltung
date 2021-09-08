using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Core.Orders.Dto;
using zbw.Auftragsverwaltung.Core.Orders.Entities;
using zbw.Auftragsverwaltung.Core.Orders.Interfaces;
using zbw.Auftragsverwaltung.Core.Orders.Contracts;


namespace zbw.Auftragsverwaltung.Core.Orders.BLL
{
    public class OrderBll : IOrderBll
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderBll(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> Get(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<PaginatedList<OrderDto>> GetList(Expression<Func<Order, bool>> predicate, int size = 10, int page = 1)
        {
            var orders = await _orderRepository.GetPagedResponseAsync(page, size, predicate);

            return _mapper.Map<PaginatedList<OrderDto>>(orders);
        }

        public async Task<PaginatedList<OrderDto>> GetList(bool deleted = false, int size = 10, int page = 1)
        {
            var orders = await _orderRepository.GetPagedResponseAsync(page, size);

            return _mapper.Map<PaginatedList<OrderDto>>(orders);
        }

        public async Task<OrderDto> Add(OrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            order = await _orderRepository.AddAsync(order);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> Delete(OrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            return await _orderRepository.DeleteAsync(order);
        }

        public async Task<OrderDto> Update(OrderDto dto)
        {
            var customer = _mapper.Map<Order>(dto);
            await _orderRepository.UpdateAsync(customer);

            return _mapper.Map<OrderDto>(customer);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Positions.Dto;
using zbw.Auftragsverwaltung.Core.Positions.Entities;
using zbw.Auftragsverwaltung.Core.Positions.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Common.Helpers;
using zbw.Auftragsverwaltung.Core.Users.Dto;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Core.Common.Exceptions;

namespace zbw.Auftragsverwaltung.Core.Positions.BLL
{
    public class PositionBll : IPositionBll
    {
        private readonly IRepository<Position, Guid> _positionRepository;
        private readonly IMapper _mapper;

        public PositionBll(IRepository<Position, Guid> positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<PositionDto> Get(Guid id)
        {
            var order = await _positionRepository.GetByIdAsync(id);
            return _mapper.Map<PositionDto>(order);
        }

        public async Task<PaginatedList<PositionDto>> GetList(Expression<Func<Position, bool>> predicate, int size = 10, int page = 1)
        {
            var orders = await _positionRepository.GetPagedResponseAsync(page, size, predicate);

            return _mapper.Map<PaginatedList<PositionDto>>(orders);
        }

        public async Task<PaginatedList<PositionDto>> GetList(bool deleted = false, int size = 10, int page = 1)
        {
            var orders = await _positionRepository.GetPagedResponseAsync(page, size);

            return _mapper.Map<PaginatedList<PositionDto>>(orders);
        }

        public async Task<PositionDto> Add(PositionDto dto)
        {
            var order = _mapper.Map<Position>(dto);
            order = await _positionRepository.AddAsync(order);

            return _mapper.Map<PositionDto>(order);
        }

        public async Task<bool> Delete(PositionDto dto)
        {
            var order = _mapper.Map<Position>(dto);
            return await _positionRepository.DeleteAsync(order);
        }

        public async Task<PositionDto> Update(PositionDto dto)
        {
            var position = _mapper.Map<Position>(dto);
            await _positionRepository.UpdateAsync(position);

            return _mapper.Map<PositionDto>(position);
        }
    }
}

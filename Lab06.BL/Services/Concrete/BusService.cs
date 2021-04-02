using AutoMapper;
using Lab06.BL.DTO.BusDTO;
using Lab06.BL.Services.Interfaces;
using Lab06.DAL.Entities;
using Lab06.DAL.UOW.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Lab06.BL.Services.Concrete
{
    public class BusService : IBusService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BusService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public BusDto GetItemById(int busId)
        {
            var bus = _uow.GetRepository<Bus>().GetItemById(busId) ?? new Bus();
            var busDto = _mapper.Map<BusDto>(bus) ?? new BusDto();

            return busDto;
        }

        public List<BusDto> GetAll()
        {
            var buses = _uow.GetRepository<Bus>().GetAll().ToList() ?? new List<Bus>();
            var busesDto = _mapper.Map<List<BusDto>>(buses);

            return busesDto;
        }
    }
}

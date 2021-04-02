using Lab06.BL.DTO.BusDTO;
using Lab06.DAL.Entities;
using System.Collections.Generic;

namespace Lab06.BL.Services.Interfaces
{
    public interface IBusService
    {
        public BusDto GetItemById(int busId);
        List<BusDto> GetAll();
    }
}

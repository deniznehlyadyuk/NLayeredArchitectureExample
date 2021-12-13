using Core.Business.DTOs.Abstract;

namespace Core.Business.DTOs
{
    public class AddressDto : IDto
    {
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string Boulevard { get; set; }
        public string Street { get; set; }
        public int BuildingNo { get; set; }
        public int RoomNo { get; set; }
    }
}
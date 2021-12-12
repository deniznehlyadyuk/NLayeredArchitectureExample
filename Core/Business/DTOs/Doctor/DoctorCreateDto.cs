namespace Core.Business.DTOs.Doctor
{
    public class DoctorCreateDto : IDto
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string IdentityNumber { get; set; }
        public AddressDto Address { get; set; }
    }
}
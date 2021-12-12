namespace Core.Business.DTOs.Doctor
{
    public class DoctorUpdateDto : IDto
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public AddressDto Address { get; set; }
    }
}
namespace Core.Business.DTOs.Person
{
    public class PersonCreateDto : IDto
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string IdentityNumber { get; set; }
    }
}
using Core.Business.DTOs.Abstract;

namespace Core.Business.DTOs.Person
{
    public class PersonUpdateDto : IDto
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
    }
}
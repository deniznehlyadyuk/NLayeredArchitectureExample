using System;

namespace Core.Business.DTOs.Student
{
    public class StudentDto : IDto
    {
        public string FullName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime Type { get; set; }
        public string SchoolNumber { get; set; }
    }
}
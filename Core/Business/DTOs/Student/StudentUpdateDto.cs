using System;

namespace Core.Business.DTOs.Student
{
    public class StudentUpdateDto : IDto
    {
        public string FullName { get; set; }
        public DateTime BornDate { get; set; }
    }
}
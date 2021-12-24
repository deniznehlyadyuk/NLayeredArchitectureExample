using System;
using Core.Business.DTOs.Student;

namespace Core.Business.DTOs.StudentScore
{
    public class StudentScoreGetDto : StudentScoreCreateDto , IEntityGetDto
    {
        public Guid Id { get; set; }
    }
}
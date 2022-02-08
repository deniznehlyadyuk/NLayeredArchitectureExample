using System;

namespace Core.Business.DTOs.Group
{
    public class GroupCreateDto : IDto
    {
        public Guid StudentId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid LessonId { get; set; }
    }
}
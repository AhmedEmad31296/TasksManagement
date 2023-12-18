using System.ComponentModel.DataAnnotations;

namespace TasksManagement.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
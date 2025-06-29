using HospitalSystem.Models.Core;

namespace HospitalSystem.Models.Dto
{
    public class InformationAboutUserDto
    {
        public List<string> Description { get; set; }
        public List<string> NameDisease { get; set; }
        public List<string> DescriptionDisease { get; set; }
        public List<DateTime> VisitDate { get; set; }
        public List<Doctor> Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}

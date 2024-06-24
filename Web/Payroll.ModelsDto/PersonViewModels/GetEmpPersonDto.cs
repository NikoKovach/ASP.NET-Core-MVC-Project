namespace Payroll.ModelsDto.PersonViewModels
{
    public class GetEmpPersonDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string LastName { get; set; }

        public string GenderType { get; set; }

        public string EGN { get; set; }

        public string? PhotoFilePath { get; set; }

    }
}

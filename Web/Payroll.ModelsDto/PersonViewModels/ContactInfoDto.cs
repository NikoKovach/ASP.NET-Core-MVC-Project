namespace Payroll.ModelsDto.PersonViewModels
{
    public class ContactInfoDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public string? PhoneNumberOne { get; set; }

        public string? PhoneNumberTwo { get; set; }

        public string? E_MailAddress1 { get; set; }

        public string? E_MailAddress2 { get; set; }

        public string? WebSite { get; set; }

        public bool HasBeenDeleted { get; set; }

    }
}
namespace Payroll.ModelsDto.EmployeeDtos.PersonDtos
{
     public class AddressDto
     {
          public int Id { get; set; }

          public int? PersonId { get; set; }

          public string? AddressType { get; set; }

          public string Country { get; set; }

          public string Region { get; set; }

          public string Municipality { get; set; }

          public string City { get; set; }

          public string Street { get; set; }

          public int Number { get; set; }

          public string? Entrance { get; set; }

          public int? Floor { get; set; }

          public int? ApartmentNumber { get; set; }

          public bool HasBeenDeleted { get; set; }
     }
}

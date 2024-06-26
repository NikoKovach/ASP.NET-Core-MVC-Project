﻿using Payroll.Models;

namespace Payroll.ModelsDto.EmployeeDtos.PersonDtos
{
     public class PersonDto
     {
          public int Id { get; set; }

          public int? EmployeeId { get; set; }

          public string  FirstName {get;set;}

          public string? MiddleName {get;set;}

          public string  LastName {get;set;}

          public int? GenderId { get; set; }
          public string? GenderType { get; set; } //string Gender.Type { get; set; }

          public string  EGN {get;set;}

          public  Address? PermanentAddress { get; set; }

          public  Address? CurrentAddress { get; set; }

          public string? PhotoFilePath { get; set; }

          public bool HasBeenDeleted { get; set; }
          
     }
}

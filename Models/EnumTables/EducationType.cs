﻿using System.ComponentModel.DataAnnotations;

namespace Payroll.Models.EnumTables
{
    public class EducationType
    {
          [Key]
          public int Id { get; set; }

          public string Type { get; set; }

          public ICollection<Diploma> Diplomas { get; set; } = new HashSet<Diploma>();

          public bool HasBeenDeleted { get; set; }
    }
}
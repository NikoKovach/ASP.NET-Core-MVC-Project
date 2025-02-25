﻿using System.ComponentModel.DataAnnotations;

namespace Payroll.Models.EnumTables
{
       public class ContractType
       {
              [Key]
              public int Id { get; set; }

              [Required]
              public string Type { get; set; }

              public ICollection<EmploymentContract>? EmploymentContracts { get; set; }
                         = new HashSet<EmploymentContract>();

              public ICollection<Annex>? Annexes { get; set; } = new HashSet<Annex>();

              public bool HasBeenDeleted { get; set; }

              public DateTime? DeletionDate { get; set; }
       }
}
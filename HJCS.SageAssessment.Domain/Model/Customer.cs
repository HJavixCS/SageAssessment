using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HJCS.SageAssessment.Domain.Model
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string CustomerNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

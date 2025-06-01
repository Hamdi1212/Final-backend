using System.ComponentModel.DataAnnotations;

namespace app.models
{
    public class Worker
    {
        [Key]
        public Guid WorkId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime dateOfBirth { get; set; }

        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Address { get; set; }

        public string Departement { get; set; }

        public string position { get; set; }

        public DateTime HireDate { get; set; }

        public int salary { get; set; }

        public string EmployeeStatus { get; set; }

        public string skills { get; set; }  

        public string certification { get; set; }
    }
}

namespace HJCS.SageAssessment.ClientMVC.Models
{
    public class Customer
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}

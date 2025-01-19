using System;

namespace PromoCodeFactory.WebHost.Models
{
    public class UpdateEmployeeEmail
    {
        public Guid EmployeeId { get; set; }
        public string NewEmail { get; set; }
    }
}
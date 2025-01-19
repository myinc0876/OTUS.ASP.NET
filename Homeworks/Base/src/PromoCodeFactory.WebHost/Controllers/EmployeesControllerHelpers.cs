using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;
using System.Collections.Generic;
using System.Linq;

internal static class EmployeesControllerHelpers
{
    public static EmployeeResponse ToEmployeeResponse(Employee employee)
    {
        return new EmployeeResponse()
        {
            Id = employee.Id,
            Email = employee.Email,
            Roles = employee.Roles?.Select(x => new RoleItemResponse()
            {
                Name = x.Name,
                Description = x.Description
            }).ToList() ?? new List<RoleItemResponse>(),
            FullName = employee.FullName,
            AppliedPromocodesCount = employee.AppliedPromocodesCount
        };
    }
}
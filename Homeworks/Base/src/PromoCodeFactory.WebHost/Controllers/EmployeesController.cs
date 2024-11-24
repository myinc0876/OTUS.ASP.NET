using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return employeesModelList;
        }

        /// <summary>
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            EmployeeResponse employeeModel = EmployeesControllerHelpers.ToEmployeeResponse(employee);

            return employeeModel;

        }

        /// <summary>
        /// Добавить новго сотрудника
        /// </summary>
        /// <param name="createEmployeeModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeResponse>> CreateEmployeeAsync(CreateEmployeeModel createEmployeeModel)
        {
            Guid id = Guid.NewGuid();
            await _employeeRepository.Create(new Employee
            {
                Id = id,
                FirstName = createEmployeeModel.FirstName,
                LastName = createEmployeeModel.LastName,
                Email = createEmployeeModel.Email,
            });

            var employee = await _employeeRepository.GetByIdAsync(id);
            EmployeeResponse employeeResponse = EmployeesControllerHelpers.ToEmployeeResponse(employee);
            return employeeResponse;
        }

        /// <summary>
        /// Обновить Email у сотрудника
        /// </summary>
        /// <param name="updateEmployeeEmail"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<EmployeeResponse>> UpdateEmployeeEmailAsync(UpdateEmployeeEmail updateEmployeeEmail)
        {
            var employee = await _employeeRepository.GetByIdAsync(updateEmployeeEmail.EmployeeId);
            employee.Email = updateEmployeeEmail.NewEmail;

            EmployeeResponse employeeResponse = EmployeesControllerHelpers.ToEmployeeResponse(employee);
            return employeeResponse;
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<bool>> UpdateEmployeeEmailAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null) return false;
            await _employeeRepository.Delete(employee);
            return true;
        }
    }
}
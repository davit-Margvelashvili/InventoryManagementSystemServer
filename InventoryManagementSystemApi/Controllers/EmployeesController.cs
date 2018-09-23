using DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagementSystemApi.Controllers
{
    public class EmployeesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetEmployees()
        {
            using (var entities = new ErpDBEntities())
            {
                return Request.CreateResponse(HttpStatusCode.OK, entities.TblEmployees.ToList());
            }

        }


        [HttpGet]
        public HttpResponseMessage GetEmployeeById(int id)
        {
            using (var entities = new ErpDBEntities())
            {
                var employee = entities.TblEmployees.FirstOrDefault(e => e.Id == id);
                if (employee != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with id={id} not found");
                }
            }
        }


        [HttpGet]
        public HttpResponseMessage GetEmployeeByPrivateNumber(string privateNumber)
        {

            using (var entities = new ErpDBEntities())
            {
                var employee = entities.TblEmployees.FirstOrDefault(e => e.PrivateNumber == privateNumber);
                if (employee != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with private number = {privateNumber} not found");
                }
            }
        }


        [HttpPut]
        public HttpResponseMessage UpdateEmployeeById(int id, TblEmployee employee)
        {
            try
            {
                using (var entities = new ErpDBEntities())
                {
                    var entity = entities.TblEmployees.FirstOrDefault(e => e.Id == id);
                    if (entity != null)
                    {
                        UpdateEmployee(employee, entity);

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with id={id} not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateEmployeeByPrivateNumber(string  privateNumber, TblEmployee employee)
        {

            try
            {
                using (var entities = new ErpDBEntities())
                {
                    var entity = entities.TblEmployees.FirstOrDefault(e => e.PrivateNumber == privateNumber);
                    if (entity != null)
                    {
                        UpdateEmployee(employee, entity);

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with private number = {privateNumber} not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPost]
        public HttpResponseMessage AddEmployee(TblEmployee employee)
        {
            try
            {
                using (var entities = new ErpDBEntities())
                {
                    entities.TblEmployees.Add(employee);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, employee);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpDelete]
        public HttpResponseMessage DeleteEmployeeById(int id)
        {
            using (var entities = new ErpDBEntities())
            {
                var employee = entities.TblEmployees.FirstOrDefault(e => e.Id == id);
                if (employee != null)
                {
                    entities.TblEmployees.Remove(employee);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with id={id} not found");
                }
            }
        }


        [HttpDelete]
        public HttpResponseMessage DeleteEmployeeByPrivateNumber(string privateNumber)
        {
            using (var entities = new ErpDBEntities())
            {
                var employee = entities.TblEmployees.FirstOrDefault(e => e.PrivateNumber == privateNumber);
                if (employee != null)
                {
                    entities.TblEmployees.Remove(employee);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with private number = {privateNumber} not found");
                }
            }
        }

        private static void UpdateEmployee(TblEmployee employee, TblEmployee entity)
        {
            entity.FirstName = employee.FirstName;
            entity.LastName = employee.LastName;
            entity.PrivateNumber = employee.PrivateNumber;
            entity.PhoneNumber = employee.PhoneNumber;
            entity.BirthDate = employee.BirthDate;
            entity.HireDate = employee.HireDate;
            entity.RetirementDate = employee.RetirementDate;
            entity.Salary = employee.Salary;
        }




    }
}

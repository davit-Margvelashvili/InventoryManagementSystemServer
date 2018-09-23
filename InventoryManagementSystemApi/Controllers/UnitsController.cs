using DatabaseAccess;
using InventoryManagementSystemApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagementSystemApi.Controllers
{
    public class UnitsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetUnits()
        {
            using (var entities = new ErpDBEntities())
            {
                var units = entities.TblUnits.Select(u => new UnitModel
                {
                    Id = u.Id,
                    Description = u.Unit
                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, units);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddUnit([FromBody]string unitName)
        {
            try
            {
                using (var entities = new ErpDBEntities())
                {
                    if (entities.TblUnits.Any(u => u.Unit.Equals(unitName, StringComparison.OrdinalIgnoreCase)))
                        return Request.CreateResponse(HttpStatusCode.Found, $"Unit with name \"{unitName}\" Already Exists");
                    else
                    {
                        var newUnit = new TblUnit
                        {
                            Unit = unitName
                        };
                        entities.TblUnits.Add(newUnit);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, newUnit);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage EditUnit(int id, string newName)
        {
            try
            {
                using (var entities = new ErpDBEntities())
                {
                    var entity = entities.TblUnits.FirstOrDefault(u => u.Id == id);
                    if (entity != null)
                    {
                        entity.Unit = newName;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpDelete]
        public HttpResponseMessage DeleteUnit(int id)
        {
            try
            {
                using (var entities = new ErpDBEntities())
                {
                    var entity = entities.TblUnits.FirstOrDefault(u => u.Id == id);
                    if (entity != null)
                    {
                        entities.TblUnits.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}

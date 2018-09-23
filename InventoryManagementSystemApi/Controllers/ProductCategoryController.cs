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
    public class ProductCategoryController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetProductCategories()
        {
            try
            {
                using (var entities = new ErpDBEntities())
                {
                    var result = entities.TblProductCategories.Select(c => new CategoryModel
                    {
                        Id = c.Id,
                        Description = c.Description
                    }).ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);

            }
        }


        [HttpPost]
        public HttpResponseMessage AddProductCategory([FromBody]string productCategory)
        {
            try
            {
                using (var entities = new ErpDBEntities())
                {
                    if (entities.TblProductCategories.Any(c => c.Description.Equals(productCategory, StringComparison.OrdinalIgnoreCase)))
                        return Request.CreateResponse(HttpStatusCode.Found, $"Unit with name \"{productCategory}\" Already Exists");
                    else
                    {
                        var newCategory = new TblProductCategory
                        {
                            Description = productCategory
                        };
                        entities.TblProductCategories.Add(newCategory);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, newCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPut]
        public HttpResponseMessage EditProductCategoryById(int id, string productCategory)
        {
            using (var entities = new ErpDBEntities())
            {
                var category = entities.TblProductCategories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    category.Description = productCategory;
                    return Request.CreateResponse(HttpStatusCode.OK, category);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Category with id = {id} not found");

                }
            }
        }


        [HttpPut]
        public HttpResponseMessage EditProductCategoryByName(string oldName, string newName)
        {
            try
            {
                using (var entities = new ErpDBEntities())
                {
                    var category = entities.TblProductCategories.FirstOrDefault(c => c.Description == oldName);
                    if (category != null)
                    {
                        category.Description = newName;
                        return Request.CreateResponse(HttpStatusCode.OK, category);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, $"Category with name \"{oldName}\" not found");
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


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
    public class ProductsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetProducts()
        {
            using (var entities = new ErpDBEntities())
            {
                var result = entities.TblProducts.Select(p => new ProductModel
                {
                    CategoryId = p.CategoryId,
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    UnitId = p.UnitId
                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetProductsByCategory(int categoryId)
        {
            using (var entities = new ErpDBEntities())
            {
                var products = entities.TblProducts.Where(p => p.CategoryId == categoryId).Select(p => new ProductModel
                {
                    Id = p.Id,
                    UnitId = p.UnitId,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryId =p.CategoryId
                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, products);
            }
        }

       // [HttpGet]
        //public HttpResponseMessage GetProductsById(int id)
        //{
        //    try
        //    {
        //        using (var entities = new ErpDBEntities())
        //        {
        //            var product = entities.TblProducts.FirstOrDefault(p => p.Id == id);
        //            if (product != null)
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK, product);
        //            }
        //            else
        //            {
        //                return Request.CreateResponse(HttpStatusCode.NotFound, $"Product with id = {id} not found");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}


        [HttpPost]
        public HttpResponseMessage AddProduct(ProductModel product)
        {
            try
            {
                using (var entities = new ErpDBEntities())
                {
                    var newProduct = new TblProduct
                    {
                        CategoryId = product.CategoryId,
                        Price = product.Price,
                        Name = product.Name,
                        UnitId = product.UnitId,
                    };
                    entities.TblProducts.Add(newProduct);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, product);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage EditProduct(int id, ProductModel product)
        {
            try
            {
                using (var entities = new ErpDBEntities())
                {
                    var entity = entities.TblProducts.FirstOrDefault(p => p.Id == id);
                    if (entity != null)
                    {
                        entity.Name = product.Name;
                        entity.CategoryId = product.CategoryId;
                        entity.UnitId = product.UnitId;
                        entity.Price = product.Price;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, $"Product with id = {id} not found");
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

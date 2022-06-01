using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Plutonium.Classes;
using Plutonium.Interfaces;
using Plutonium.Services;
using Microsoft.Extensions.Options;

namespace Plutonium.Bases
{


    public class JSONController<T> : Controller, IJSONController
       where T : class
    {
        private DBContext db;
        private readonly IOptions<AppConfiguration> _appConfiguration;

        public JSONController(IOptions<AppConfiguration> appConfiguration, DBContext context)
        {
            _appConfiguration = appConfiguration;
            db = context;
            db.Database.EnsureCreated();
        }

        public virtual JsonResult GetItems()
        {
            try
            {
                return Json(new { Result = "OK", Records = db.Set<T>().AsEnumerable() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });
            }
        }

        public JsonResult GetItems(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                string sortName = "Id";
                string SortAscDesc = "ASC";
                IEnumerable<T> results;

                // Pagination
                if (jtPageSize == 0)
                    results = db.Set<T>().AsEnumerable();  // No Pagination
                else
                    results = db.Set<T>().AsEnumerable().Skip(jtStartIndex).Take(jtPageSize); // Pagination

                // Sorting
                if (jtSorting != null)
                {
                    sortName = jtSorting.Split(' ')[0].ToString();
                    SortAscDesc = jtSorting.Split(' ')[1].ToString();
                    var pi = typeof(T).GetProperty(sortName);
                    if (SortAscDesc == "ASC")
                    {
                        results = results.OrderBy(x => pi.GetValue(x, null));
                    }
                    else
                    {
                        results = results.OrderByDescending(x => pi.GetValue(x, null));

                    }
                }

                int recordCount = db.Set<T>().AsEnumerable().Count();

                return Json(new { Result = "OK", Records = results, TotalRecordCount = recordCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });
            }
        }


        public virtual JsonResult Create(object o)
        {
            if (o is T) // Just to be sure, and maybe throw an exception
                return Create(o as T);
            else
                return Json(new { Result = "ERROR", Message = "Create object passed is not correct" });
        }
        public virtual JsonResult Create(T item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Set<T>().Add(item);
                    db.SaveChanges();
                    return Json(new { Result = "OK", Record = item });
                }
                else
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return Json(new { Result = "ERROR", Message = "Model State is not valid : " + message });
                }
            }
            catch (Exception ex)
            {
                //return Json(new { Result = "ERROR", Message = ex.Message });
                return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });

            }
        }

        public virtual JsonResult Edit(object o)
        {
            if (o is T) // Just to be sure, and maybe throw an exception
                return Edit(o as T);
            else
                return Json(new { Result = "ERROR", Message = "Edit object passed is not correct" });
        }

        public virtual JsonResult Edit(T item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return Json(new { Result = "ERROR", Message = "Model State is not valid : " + message });
                }
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });
            }
        }

        public virtual JsonResult Delete(int id)
        {
            try
            {
                T item = db.Set<T>().Find(id);
                if (item == null)
                    return Json(new { Result = "ERROR", Message = "Not Found" });

                db.Set<T>().Remove(item);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });
            }
        }


    }


    public abstract class BaseJSONController<T> : Controller
  where T : class
    {

        private DBContext db;
        public IOptions<AppConfiguration>  _appConfiguration;

        public BaseJSONController(IOptions<AppConfiguration> appConfiguration, DBContext context)
        {
            _appConfiguration = appConfiguration;
            db = context;
            db.Database.EnsureCreated();
        }
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual JsonResult GetItems()
        {
            try
            {
                return Json(new { Result = "OK", Records = db.Set<T>().AsEnumerable() });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });
            }
        }


        // USED FOR PAGINATION
        [HttpPost]
        public JsonResult GetItems(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                string sortName = "Id";
                string SortAscDesc = "ASC";
                IEnumerable<T> results;

                // Pagination
                if (jtPageSize == 0)
                    results = db.Set<T>().AsEnumerable();  // No Pagination
                else
                    results = db.Set<T>().AsEnumerable().Skip(jtStartIndex).Take(jtPageSize); // Pagination

                // Sorting
                if (jtSorting != null)
                {
                    sortName = jtSorting.Split(' ')[0].ToString();
                    SortAscDesc = jtSorting.Split(' ')[1].ToString();
                    var pi = typeof(T).GetProperty(sortName);
                    if (SortAscDesc == "ASC")
                    {
                        results = results.OrderBy(x => pi.GetValue(x, null));
                    }
                    else
                    {
                        results = results.OrderByDescending(x => pi.GetValue(x, null));

                    }
                }

                int recordCount = db.Set<T>().AsEnumerable().Count();

                return Json(new { Result = "OK", Records = results, TotalRecordCount = recordCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });
            }
        }

        [HttpPost]
        public virtual JsonResult Create(T item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Set<T>().Add(item);
                    db.SaveChanges();
                    return Json(new { Result = "OK", Record = item });
                }
                else
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return Json(new { Result = "ERROR", Message = "Model State is not valid : " + message });
                }
            }
            catch (Exception ex)
            {
                //return Json(new { Result = "ERROR", Message = ex.Message });
                return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });

            }
        }

        [HttpPost]
        public virtual JsonResult Edit(T item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return Json(new { Result = "ERROR", Message = "Model State is not valid : " + message });
                }
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });
            }
        }

        [HttpPost]
        public virtual JsonResult Delete(int id)
        {
            try
            {
                T item = db.Set<T>().Find(id);
                if (item == null)
                    return Json(new { Result = "ERROR", Message = "Not Found" });

                db.Set<T>().Remove(item);
                db.SaveChanges();
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });
            }
        }

    }

}
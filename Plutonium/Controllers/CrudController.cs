using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Plutonium.Classes;
using Plutonium.Helpers;
using Plutonium.Services;
using Plutonium.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plutonium.Controllers
{
    public class CrudController : Controller
    {
        List<CRUDModel> crudModels;
        List<CRUDField> crudFields;

        private DBContext db ;
        private readonly IOptions<AppConfiguration> _appConfiguration;

        public CrudController(IOptions<AppConfiguration> appConfiguration, DBContext context)
        {
            _appConfiguration = appConfiguration;
            db = context;

            db.Database.EnsureCreated();

            var theList = Assembly.GetExecutingAssembly().GetTypes()
                    .Where(t => t.Namespace == "Plutonium.Models")
                    .ToList();
            crudModels = new List<CRUDModel>();
            crudFields = new List<CRUDField>();
            foreach (var type in theList)
            {
                var properties = type.GetProperties();
                CRUDModel model = new CRUDModel();

                var modelName = type.Name;
                model.Name = modelName;
                model.Label = AttributeHelper.GetDisplayAttributeName(type);

                crudModels.Add(model);
                foreach (var property in properties)
                {
                    CRUDField crudField = new CRUDField();
                    crudField.ModelName = modelName;
                    crudField.Name = property.Name;
                    crudField.Label = AttributeHelper.GetDisplayAttributeName(property);
                    crudField.Type = AttributeHelper.GetPropertyTypeName(property);
                    crudField.Ordering = AttributeHelper.GetPropertyOrdering(property);
                    crudField.IsVisible = AttributeHelper.GetPropertyIsVisible(property);
                    crudFields.Add(crudField);
                }

            }
        }


        public IActionResult Index(string modelName)
        {
            CrudViewModel vm = new CrudViewModel();
            vm.CRUDModel = crudModels.Where(x => x.Name.Equals(modelName)).FirstOrDefault();
            if (vm.CRUDModel != null)
            {
                vm.CRUDFields = crudFields.Where(x => x.ModelName.Equals(modelName)).ToList();
                vm.ModelName = modelName;
                /*
                //string assemblyQualifiedName = "Plutonium.Models.Link, Plutonium, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null";
                //assemblyQualifiedName = typeof(Models.Link).AssemblyQualifiedName;
                string assemblyQualifiedName = "Plutonium.Models.Link, Plutonium";
                // Type type = Type.GetType(assemblyQualifiedName);
                //var instaniatedObject = Activator.CreateInstance(type);

                //  var object = Activator.CreateInstance(type);
                // Bases.BaseJSONController<type> baseJSONController = new Bases.BaseJSONController<type>();

                Type typeArgument = Type.GetType(assemblyQualifiedName, true, true);
                Type template = typeof(Bases.JSONController<>);
                Type genericType = template.MakeGenericType(typeArgument);
                var instance = Activator.CreateInstance(genericType) as Interfaces.IJSONController;


                JsonResult result = instance.GetItems();
                */
                return View(vm);
            }
            else
            {
                //Redirect to Error
                return RedirectToAction("NotFound");
            }
        }

        public JsonResult GetItems(string modelName)
        {


            //string assemblyQualifiedName = "Plutonium.Models.Link, Plutonium, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null";
            //assemblyQualifiedName = typeof(Models.Link).AssemblyQualifiedName;
            string assemblyQualifiedName = String.Format("Plutonium.Models.{0}, Plutonium", modelName);
            //  Type type = Type.GetType(assemblyQualifiedName);
            //  var instaniatedObject = Activator.CreateInstance(type);

            //  var object = Activator.CreateInstance(type);
            // Bases.BaseJSONController<type> baseJSONController = new Bases.BaseJSONController<type>();

            Type typeArgument = Type.GetType(assemblyQualifiedName, true, true);

            Type template = typeof(Bases.JSONController<>);
            Type genericType = template.MakeGenericType(typeArgument);

            //Type interfacetemplate = typeof(Interfaces.IJSONController<>);
            //Type genericInterfaceType = interfacetemplate.MakeGenericType(typeArgument); 

            var instance = Activator.CreateInstance(genericType) as Interfaces.IJSONController; // genericInterfaceType;
            JsonResult result = instance.GetItems();
            return result;

            //   Microsoft.EntityFrameworkCore.DbSet mySet = db.Set(Type.GetType("<Your Entity Name>"));

            //try
            //{
            //    return Json(new { Result = "OK", Records = db.Set <type> ().AsEnumerable() });
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { Result = "ERROR", Message = ex.GetBaseException().Message });
            //}

            //

        }


        public JsonResult Create(string modelName, object o)
        {

             
            string assemblyQualifiedName = String.Format("Plutonium.Models.{0}, Plutonium", modelName);
             
            Type typeArgument = Type.GetType(assemblyQualifiedName, true, true);
            Type template = typeof(Bases.JSONController<>);
            Type genericType = template.MakeGenericType(typeArgument);
             
            var instance = Activator.CreateInstance(genericType) as Interfaces.IJSONController;  
            JsonResult result = instance.Create(o);
            return result;
             

        }
    }
}

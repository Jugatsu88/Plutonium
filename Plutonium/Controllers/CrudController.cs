using Microsoft.AspNetCore.Mvc;
using Plutonium.Classes;
using Plutonium.Helpers;
using Plutonium.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plutonium.Controllers
{
    public class CrudController : Controller
    {
        List<CRUDModel> crudModels  ;
        List<CRUDField> crudFields  ;
        public CrudController()
        {
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
                model.Label=AttributeHelper.GetDisplayAttributeName(type);

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
                return View(vm);
            }
            else
            {
                //Redirect to Error
                return RedirectToAction("NotFound"); 
            } 
        }
    }
}

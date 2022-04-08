using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plutonium.Interfaces
{

    public interface IJSONController
    {
        public JsonResult GetItems();

        public JsonResult GetItems(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null);

        public JsonResult Create(object o);
        //public JsonResult Create(T item);

        public JsonResult Edit(object o);
        //public JsonResult Edit(T item);

        public JsonResult Delete(int id);

    }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Plutonium.Bases;
using Plutonium.Classes;
using Plutonium.Models;
using Plutonium.Services;

namespace Plutonium.Controllers
{
    public class ProcessController : BaseJSONController<Process>
    {
        public ProcessController(IOptions<AppConfiguration> appConfiguration, DBContext context) : base(appConfiguration, context)
        {
        }
    }
}

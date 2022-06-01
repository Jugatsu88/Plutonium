using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Plutonium.Bases;
using Plutonium.Classes;
using Plutonium.Models;
using Plutonium.Services;

namespace Plutonium.Controllers
{

    public class BatchFileController : BaseJSONController<BatchFile>
    {
        public BatchFileController(IOptions<AppConfiguration> appConfiguration, DBContext context) : base(appConfiguration, context)
        {
        }
    }
}
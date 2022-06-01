using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Plutonium.Bases;
using Plutonium.Classes;
using Plutonium.Models;
using Plutonium.Services;

namespace Plutonium.Controllers
{
    public class LinkController : BaseJSONController<Link>
    {
        public LinkController(IOptions<AppConfiguration> appConfiguration, DBContext context) : base(appConfiguration, context)
        {
        }
    }
}
using IW5.API.Controllers.Base;
using IW5.DAL.Contracts;
using IW5.Models.Entities;
using Microsoft.AspNetCore.Components;
namespace IW5.API.Controllers
{

    [Route("api/[controller]")]
    public class OptionsController : BaseCrudController<Option, OptionsController>
    {

        public OptionsController(IOptionRepository carRepo) : base(carRepo)
        {
        }

    }
}

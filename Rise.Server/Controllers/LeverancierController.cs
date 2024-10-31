using Microsoft.AspNetCore.Mvc;
using Rise.Shared.Leveranciers;

using Rise.Shared.Products;

namespace Rise.Server.Controllers
{
    public class LeverancierController : ControllerBase
    {
        private readonly ILeverancierService leverancierService;
        
        public LeverancierController (ILeverancierService leverancierService)
        {
            this.leverancierService = leverancierService;
        }

        [HttpGet]
        public async Task<IEnumerable<LeverancierDto>> Get()
        {
            var leveranciers = await leverancierService.GetLeveranciersAsync();
            return leveranciers;
        }
    }
}

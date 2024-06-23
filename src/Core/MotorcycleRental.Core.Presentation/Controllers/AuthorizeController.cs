using Microsoft.AspNetCore.Authorization;

namespace MotorcycleRental.Core.Presentation.Controllers;

[Authorize]
public abstract class AuthorizeController : BaseController
{

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JournalApp.DAL;
using JournalApp.Models;
using JournalApp.Models.FormModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JournalApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    public class AccountsController : Controller
    {
	    private readonly ApplicationDbContext _appDbContext;
	    private readonly UserManager<AppUser> _userManager;
	    private readonly IMapper _mapper;

	    public AccountsController(UserManager<AppUser> userManager, IMapper mapper, ApplicationDbContext appDbContext)
	    {
		    _userManager = userManager;
		    _mapper = mapper;
		    _appDbContext = appDbContext;
	    }

	    // POST api/accounts
	    [HttpPost]
	    public async Task<IActionResult> Post([FromBody]EmailRegistrationForm model)
	    {
		    if (!ModelState.IsValid)
		    {
			    return BadRequest(ModelState);
		    }

		    var userIdentity = _mapper.Map<AppUser>(model); // Mapping from EmailRegistrationForm to AppUser model

		    var result = await _userManager.CreateAsync(userIdentity, model.Password);

		    if (!result.Succeeded) return new BadRequestObjectResult(result.Errors);

		    await _appDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
		    await _appDbContext.SaveChangesAsync();

		    return new OkObjectResult("Account created");
	    }
	}
}
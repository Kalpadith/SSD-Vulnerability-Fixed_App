using Microsoft.AspNetCore.Mvc;
using DatabaseConfigClassLibrary.DTO;
using Microsoft.AspNetCore.Authorization;
using CustomerDetailsManagementApp.Services;
using Microsoft.AspNetCore.Mvc.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using DatabaseConfigClassLibrary.DatabaseConfig;
using DatabaseConfigClassLibrary.Repositories;
using DatabaseConfigClassLibrary.RepositoryImpl;
using CustomerDetailsManagementApp.Services.ServiceInterfaces;

namespace CustomerDetailsManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;
        private readonly ILoginRequestService _loginRequestService;
        private readonly IEditUserService _editUserService;
        private readonly IGetDistanceService _getDistanceService;
        private readonly ISearchUserService _searchUserService;
        private readonly IGetCustomerListByZipCodeService _getCustomerListService;
        private readonly IGetAllCustomerListService _getAllCustomerListService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(
            ApplicationDbContext context,
            IConfiguration configuration,
            ILoginService loginService,
            ILoginRequestService loginRequestService,
            IEditUserService editUserService,
            IGetDistanceService getDistanceService,
            ISearchUserService searchUserService,
            IGetCustomerListByZipCodeService getCustomerListService,
            IGetAllCustomerListService getAllCustomerListService,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            _context = context;
            _configuration = configuration;
            _loginService = loginService;
            _loginRequestService = loginRequestService;
            _editUserService = editUserService;
            _getDistanceService = getDistanceService;
            _searchUserService = searchUserService;
            _getCustomerListService = getCustomerListService;
            _getAllCustomerListService = getAllCustomerListService;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("Login")]
        [Route("v{version:apiVersion}/Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestService _loginRequestService)
        {
            if (_loginRequestService != null)
            {
                var user = await _userRepository.FindByNameAsync(_loginRequestService.Username);

                if (user != null && await _userRepository.CheckPasswordAsync(user, _loginRequestService.Password))
                {
                    var roles = await _userRepository.GetRolesAsync(user);
                    var token = _loginService.GenerateJwtToken(
                        _loginRequestService.Username,
                        roles.FirstOrDefault()
                    );

                    return Ok(new { access_token = token });
                }
            }

            return Unauthorized("Invalid username or password");
        }


        // PUT api/User/EditUser/{_id}
        [Authorize(Roles = "Admin,Client")]
        [HttpPut]
        [MapToApiVersion("1.0")]
        [Route("EditUser/{_id}")]
        [Route("v{version:apiVersion}/EditUser/{_id}")]
        public async Task<IActionResult> EditUser(string _id, [FromBody] UserUpdateDTO userUpdate)
        {
            try
            {
                var (success, message) = await _editUserService.EditUserAsync(_id, userUpdate);

                if (success)
                {
                    return Ok(message);
                }

                return BadRequest(message);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        //GET api/User/GetDistance/Id?Latitude=value&Longitude=value
        [Authorize(Roles = "Admin,Client")]
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetDistance/{Id}")]
        [Route("v{version:apiVersion}/GetDistance/{Id}")]
        public IActionResult GetDistance(string Id, double latitude, double longitude)
        {
            try
            {
                var userLatitude = _userRepository.GetLatitude(Id);
                var userLongitude = _userRepository.GetLongitude(Id);

                if (userLatitude.HasValue && userLongitude.HasValue)
                {
                    double userLat = userLatitude.Value;
                    double userLon = userLongitude.Value;

                    double distanceInKilometers = _getDistanceService.CalculateDistance(
                        userLat,
                        userLon,
                        latitude,
                        longitude
                    );

                    return Ok(distanceInKilometers);
                }
                else
                {
                    return BadRequest("User's Latitude or Longitude is missing.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        //GET api/User/SearchUser?searchText=text to search
        [Authorize(Roles = "Admin,Client")]
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("SearchUser")]
        [Route("v{version:apiVersion}/SearchUser")]
        public IActionResult SearchUser(string searchText)
        {
            try
            {
                var matchedUsers = _searchUserService.SearchUsers(searchText);
                return Ok(matchedUsers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        //GET api/User/GetCustomerListByZipCode
        [Authorize(Roles = "Admin,Client")]
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetCustomerListByZipCode")]
        [Route("v{version:apiVersion}/GetCustomerListByZipCode")]
        public IActionResult GetCustomerListByZipCode()
        {
            try
            {
                var result = _getCustomerListService.GetCustomersByZipCode();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        //GET api/User/GetAllCustomerList
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [MapToApiVersion("1.0")]
        [Route("GetAllCustomerList")]
        [Route("v{version:apiVersion}/GetAllCustomerList")]
        public IActionResult GetAllCustomerList()
        {
            try
            {
                var customerList = _getAllCustomerListService.GetAllCustomersAndAddresses();
                return Ok(customerList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}

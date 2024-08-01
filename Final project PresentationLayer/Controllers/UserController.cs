using Final_project_PresentationLayer.Models;
using Final_project_PresentationLayer.Models.Profile_Update_View_Model;
using Final_project_PresentationLayer.Models.Registration_View_Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Final_project_PresentationLayer.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult UpdateProfile()
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                var role = jwtToken?.Claims.First(claim => claim.Type == "Roles")?.Value;

                if (role == RoleConst.Individual)
                {
                    return RedirectToAction("IndividualUpdate");
                }
                else if (role == RoleConst.OrganizationHead)
                {
                    return RedirectToAction("OrganizationHeadUpdate");
                }
                else if (role == RoleConst.FamilyHead)
                {
                    return RedirectToAction("FamilyHeadUpdate");
                }
                else if (role == RoleConst.Manager)
                {
                    return RedirectToAction("ManagerUpdate");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }

            return BadRequest("User Not Found");
        }

        public IActionResult OrganizationHeadUpdate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrganizationHeadUpdate(OrganizatioHeadUpdateViewModel request)
        {

            var apiUrl = "https://localhost:7005/api/Profile/UpdateProfile-OrganizationHead";
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);


            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "profile Update successful.";
                return View(request);
            }
            else
            {
                TempData["Message"] = "Profile Update failed.";
            }
        
            return View();
        }

        public IActionResult ManagerUpdate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ManagerUpdate(ManagerUpdateViewModel request)
        {
            var apiUrl = "https://localhost:7005/api/Profile/UpdateProfile-Manager";
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);


            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "profile Update successful.";
                return View(request);
            }
            else
            {
                TempData["Message"] = "Profile Update failed.";
            }

            return View();
        }


        public IActionResult IndividualUpdate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndividualUpdate(IndividualUpdateViewModel request)
        {
            var apiUrl = "https://localhost:7005/api/Profile/UpdateProfile-Individual";
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);


            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "profile Update successful.";
                return View(request);
            }
            else
            {
                TempData["Message"] = "Profile Update failed.";
            }

            return View();
        }


        public IActionResult FamilyHeadUpdate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FamilyHeadUpdate(FamilyHeadViewModel request)
        {
            var apiUrl = "https://localhost:7005/api/Profile/UpdateProfile-FamilyHead";
            string json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);


            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "profile Update successful.";
                return View(request);
            }
            else
            {
                TempData["Message"] = "Profile Update failed.";
            }

            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> ViewProfile()
        {
            var apiUrl = "https://localhost:7005/api/Profile/View-Profile";
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _httpClient.GetAsync($"{apiUrl}?userId={userId}");
            if (!response.IsSuccessStatusCode)
            {
                // Handle error
                TempData["Message"] = "Unable to retrieve profile.";
                return RedirectToAction("Error", "Home");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var profileData = JsonConvert.DeserializeObject<ViewProfileViewModel>(responseContent);

            // Determine which view to return based on the user's role
            switch (profileData.Role)
            {
                case "Individual":
                    return View("IndividualProfile", profileData);

                case "OrganizationHead":
                    return View("OrganizationProfile", profileData);

                case "FamilyHead":
                    return View("FamilyHeadProfile", profileData);

                case "Manager":
                    return View("ManagerProfile", profileData);

                default:
                    return RedirectToAction("Error", "Home");
            }

        }
    }
}

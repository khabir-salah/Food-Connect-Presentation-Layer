using Final_project_PresentationLayer.Models.DonationViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Final_project_PresentationLayer.Controllers
{
    public class DonationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DonationController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult CreateDonation()
        {
            var token = HttpContext.Session.GetString("JWToken");

            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                var id = jwtToken?.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
                Guid.TryParse(id, out Guid userId);
                var model = new CreateDonationViewModel
                {
                    UserId = userId
                };
                return View(model);
            }
            return BadRequest("User Not Found");
        }

        [HttpPost]
        public async Task<IActionResult> CreateDonation(CreateDonationViewModel request)
        {
            if(ModelState.IsValid)
            {
                var apiUrl = "https://localhost:7005/api/Profile/Create-Donation";
                string json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);

                if(response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Donation Created Successfully \n Thanks for your Donation";
                    return View("Success");
                }
                TempData["Message"] = "Donation Creation failed.";
            }
            TempData["Message"] = "Invalid data \n make sure all field are complete";
            return View(request);
        }

        public async Task<IActionResult> ViewDonationType()
        {
            var apiUrl = "https://localhost:7005/api/Donation/Donation-Count";
            
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<DonationSummaryViewModel>(jsonResponse);
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PendingDonations()
        {
            var apiUrl = "https://localhost:7005/api/Donation/Pending-Donations";

            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<DonationViewModel>(jsonResponse);
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ApprovedDonations()
        {
            var apiUrl = "https://localhost:7005/api/Donation/Approved-Donation";

            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<DonationViewModel>(jsonResponse);
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReceivedDonations()
        {
            var apiUrl = "https://localhost:7005/api/Donation/Received-Donation";

            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<DonationViewModel>(jsonResponse);
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DisapprovedDonations()
        {
            var apiUrl = "https://localhost:7005/api/Donation/Disapprove-Donation";

            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<DonationViewModel>(jsonResponse);
                return View(model);
            }
            return View();
        }

        public async Task<IActionResult> TrackDonationWithMessages(Guid donationId)
        {
            var response = await _httpClient.GetAsync($"api/donations/{donationId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<DonationWithMessagesViewModel>(jsonResponse);
                return View(model);
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageViewModel model)
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(id, out Guid userId);
            var messageRequest = new SendMessageRequest
            {
                SenderId = userId,
                RecipientId = model.RecipientId,
                DonationId = model.DonationId,
                Content = model.Content
            };

            var response = await _httpClient.PostAsJsonAsync("api/messages", messageRequest);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("TrackDonationWithMessages", new { donationId = model.DonationId });
            }

            return View("Error");
        }

    }
}

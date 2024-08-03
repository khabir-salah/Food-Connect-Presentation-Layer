using Final_project_PresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using System.Text;
using static Final_project_PresentationLayer.Models.RegisterFamilyHeadViewModel;

namespace Final_project_PresentationLayer.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly HttpClient _httpClient; 
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public RegistrationController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpContextAccessor = httpContextAccessor;
        }
        public IActionResult RegisterAsOrganizationHead()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsOrganizationHead(RegisterOrganizationViewModel request)
        {

            if (ModelState.IsValid)
            {
                var apiUrl = "https://localhost:7005/api/Identity/Register-Organization";
                string json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);

                
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Registration successful. \n Please check your email to confirm your account.";
                    return RedirectToAction("Login"); 
                }
                else
                {
                    TempData["Message"] = "Registration failed. \n Ensure details are in correct Order";
                }
            }
            else
            {
                TempData["Message"] = "Invalid data provided.";
            }

            return View();
        }




        public IActionResult RegisterAsIndivual()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterASIndividual(RegisterIndividualViewModel request)
        {
            if (ModelState.IsValid)
            {
                var apiUrl = "https://localhost:7005/api/Identity/Register-Individual";
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = responseObject?.message ?? "Registration successful.";
                    //RedirectToAction("Login");
                }
                // Read the response content as a string
                //var responseContents = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to a dynamic object
                //var responseObjects = JsonConvert.DeserializeObject<dynamic>(responseContent);

                // Access the message from the response object
                TempData["Message"] = responseObject?.message ?? "Registration Failed";

                // Store the message in TempData to pass it to the view
            }

            return View();
        }

        public IActionResult RegisterAsFamilyHead()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAsFamilyHead(FamilyHeadViewModel request)
        {
            if (ModelState.IsValid)
            {
                var apiUrl = "https://localhost:7005/api/Identity/Register-Family";
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = responseObject?.message ?? "Registration successful.";
                    //RedirectToAction("Login");
                }
                // Read the response content as a string
                //var responseContents = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to a dynamic object
                //var responseObjects = JsonConvert.DeserializeObject<dynamic>(responseContent);

                // Access the message from the response object
                TempData["Message"] = responseObject?.message ?? "Registration Failed";

                // Store the message in TempData to pass it to the view
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegistrationLoginViewModel.LoginViewModel request)
        {
            if (ModelState.IsValid)
            {
                var apiUrl = "https://localhost:7005/api/Identity/Login";
                string json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);

                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                    // Store the token in a cookie or session
                    //var session = _httpContextAccessor.HttpContext.Session;
                    //session.SetString("JWToken", responseObject.Token.ToString());
                    HttpContext.Session.SetString("JWToken", responseObject.Token.ToString());

                    // Redirect to the user dashboard
                    return RedirectToAction("UserDashBoard");
                }
                else
                {
                    TempData["Message"] = "Invalid Credential provided.";
                }
            }
            else
            {
                TempData["Message"] = "Invalid data provided.";
            }
            return View();
        }

        public IActionResult UserDashBoard()
        {
            return View();
        }
    }
}

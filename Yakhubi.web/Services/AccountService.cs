using BlazorApp.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IAccountService
    {
        User User { get; }
        Task Initialize();
        Task Login(User model);
        Task Logout();
        Task Register(User model);
        Task vOtp(User model);
        Task<User> Dashboard(User model);
         Task UpdateOtp(string id, User model);
        Task<IList<User>> GetAll();
        Task<User> GetById(string id);
        Task Update(string id, User model);
        Task Delete(string id);
        Task<User> GetByEmail(string email);
        Task<bool> VerifyOtp(string username, string otp);
        Task<bool> UpdatePassword(string username, string password);
    }

    public class AccountService : IAccountService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "user";

        public User User { get; private set; }

        public AccountService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        ) {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<User>(_userKey);
        }

        public async Task Login(User model)
        {
            User = await _httpService.Post<User>("users/authenticate", model);
            // User = await _httpService.Post<User>("/users/authenticate", model);
            await _localStorageService.SetItem(_userKey, User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem(_userKey);
            _navigationManager.NavigateTo("account/login");
        }

        public async Task Register(User model)
        {
            await _httpService.Post("/reg/2", model);
            // await _httpService.Post("users/register", model);
        }
        public async Task vOtp(User model)
        {
            // await _httpService.Post("reg/2", model);
            await _httpService.Post("reg/", model);
        }

        public async Task<IList<User>> GetAll()
        {
            return await _httpService.Get<IList<User>>("/users");
        }

        public async Task<User> GetById(string id)
        {
            return await _httpService.Get<User>($"/users/{id}");
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _httpService.Get<User>($"/users/{email}");
        }

        public async Task<bool> VerifyOtp(string username, string otp)
        {
            return await _httpService.Get<bool>($"/usersbyotp/{username}/{otp}");
        }

        public async Task<bool> UpdatePassword(string username, string password)
        {
            return await _httpService.Get<bool>($"/updatepassword/{username}/{password}");
        }
        public async Task UpdateOtp(string id, User model)
        {
            if (id == User.Id) 
            {
            await _httpService.Post("/account/info", model);
            }
        
        }
        public async Task Update(string id, User model)
        {
            
            // update stored user if the logged in user updated their own record
            if (id == User.Id) 
            {
                await _httpService.Post($"/account/info/2", model);
                // update local storage
                User.Name = model.Name;
                User.Phone = model.Phone;
                User.Idno = model.Idno;
                await _localStorageService.SetItem(_userKey, User);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/users/{id}");

            // auto logout if the logged in user deleted their own record
            if (id == User.Id)
                await Logout();
        }
        public async Task<User> Dashboard(User user)
        {
            user = await _httpService.Get<User>($"/dashboard");

            return user;
        }
        
    }
}
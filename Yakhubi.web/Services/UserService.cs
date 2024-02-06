using System.IO;
using System;
using BlazorApp.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yakhubi.Models;

namespace BlazorApp.Services
{
    public interface IUserService
    {
        User User { get; }
        Task Initialize();
        Task Trans(Transaction model);
        Task<IList<Transaction>> GetAllTrans();
        Task<Transaction> GetTransById(string id);
    }

    public class UserService : IUserService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "user";

        public User User { get; private set; }

        public UserService(
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

        public async Task Trans(Transaction model)
        {
            await _httpService.Post("/transfer", model);
        }

        public async Task<IList<Transaction>> GetAllTrans()
        {
            return await _httpService.Get<IList<Transaction>>("/transactions");
        }

        public async Task<Transaction> GetTransById(string id)
        {
            return await _httpService.Get<Transaction>($"/transaction/{id}");
        }

    }
}
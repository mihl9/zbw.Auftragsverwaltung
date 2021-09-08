using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace zbw.Auftragsverwaltung.BlazorApp.Services
{
    public class CookieStorageService : IStorageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieStorageService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<T> GetItem<T>(string key)
        {
            if (_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(key, out var value))
                return await Task.FromResult(JsonSerializer.Deserialize<T>(value));

            return await Task.FromResult((T)default);
        }

        public async Task SetItem<T>(string key, T value)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value.ToString());
            await Task.CompletedTask;
        }

        public async Task RemoveItem(string key)
        {
            try
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
            }
            catch (Exception)
            {
                // ignored
            }

            await Task.CompletedTask;
        }
    }
}

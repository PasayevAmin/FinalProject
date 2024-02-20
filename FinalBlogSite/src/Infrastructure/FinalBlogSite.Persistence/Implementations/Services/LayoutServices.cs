using FinalBlogSite.Persistence.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlogSite.Persistence.Implementations.Services
{
    public class LayoutServices
    {
        public readonly AppDbContext _context;
        public LayoutServices(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<Dictionary<string, string>> GetSettingsAsync()
        //{
        //    //Dictionary<string, string> settings = await _context..ToDictionaryAsync(s => s.Key, s => s.Value);
        //    //return settings;
        //}
    }
}

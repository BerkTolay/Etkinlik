
using Etkinlik.Core.Models;
using Etkinlik.Core.Repositories;
using Etkinlik.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Etkinlik.Repository.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Company> GetByName(string name)
        {
            return await context.Companies.FirstOrDefaultAsync(x=>x.Name==name);
        }
    }
}

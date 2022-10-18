using Etkinlik.Core.IUnitOfWork;
using Etkinlik.Core.Models;
using Etkinlik.Core.Repositories;
using Etkinlik.Core.Services;

namespace Etkinlik.Service.Services
{
    public class CompanyService : Service<Company>, ICompanyService
    {
        ICompanyRepository companyRepository;       

        public CompanyService(IGenericRepository<Company> repository, IUnitOfWork unitOfWork, ICompanyRepository companyRepository) : base(repository, unitOfWork)
        {
            this.companyRepository = companyRepository;
        }

        public async Task<Company> GetByName(string name)
        {
            var company = await companyRepository.GetByName(name);
            return company;
            
        }
    }
}

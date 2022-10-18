using Etkinlik.Core.Models;

namespace Etkinlik.Core.Services
{
    public interface ICompanyService: IService<Company>
    {
        Task<Company> GetByName(string name);
    }
}

using Etkinlik.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etkinlik.Core.Repositories
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<Company> GetByName(string name);
    }
}

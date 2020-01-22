using SmartELock.Core.Domain.Models;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repository
{
    public interface ICompanyRepository
    {
        Task<int> CreateCompany(Company company);
        Task<Company> GetCompany(int companyId);
        Task<Company> GetCompany(string companyName);
    }
}

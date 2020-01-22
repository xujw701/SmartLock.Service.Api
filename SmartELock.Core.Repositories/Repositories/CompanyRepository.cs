using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Snapshots;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IDbRetryHandler _dbRetryHandler;

        public CompanyRepository(IDbRetryHandler dbRetryHandler)
        {
            _dbRetryHandler = dbRetryHandler;
        }

        public async Task<int> CreateCompany(Company company)
        {
            var id = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Company_Create", new
                {
                    company.CompanyName
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return id;
        }

        public async Task<Company> GetCompany(int companyId)
        {
            var company = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Company_Get", new
                {
                    companyId
                }))
                {
                    var snapshots = reader.Read<CompanySnapshot>().ToList();

                    return snapshots.Select(snapshot => Company.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return company;
        }

        public async Task<Company> GetCompany(string companyName)
        {
            var company = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Company_GetByCompanyName", new
                {
                    companyName
                }))
                {
                    var snapshots = reader.Read<CompanySnapshot>().ToList();

                    return snapshots.Select(snapshot => Company.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return company;
        }
    }
}

﻿using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Snapshots;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IDbRetryHandler _dbRetryHandler;

        public BranchRepository(IDbRetryHandler dbRetryHandler)
        {
            _dbRetryHandler = dbRetryHandler;
        }

        public async Task<int> CreateBranch(Branch branch)
        {
            var id = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Branch_Create", new
                {
                    branch.CompanyId,
                    branch.BranchName,
                    branch.Address
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return id;
        }

        public async Task<Branch> GetBranch(int branchId)
        {
            var branch = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Branch_Get", new
                {
                    branchId
                }))
                {
                    var snapshots = reader.Read<BranchSnapshot>().ToList();

                    return snapshots.Select(snapshot => Branch.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return branch;
        }

        public async Task<List<Branch>> GetBranchesByUserId(int userId)
        {
            var branches = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Branch_GetByUserId", new
                {
                    userId
                }))
                {
                    var snapshots = reader.Read<BranchSnapshot>().ToList();

                    return snapshots.Select(snapshot => Branch.CreateFrom(snapshot));
                }
            });

            return branches.ToList();
        }

        public async Task<bool> UpdateBranch(Branch branch)
        {
            var result = await _dbRetryHandler.QueryAsync(async connection =>
            {
                return await connection.ExecuteAsync("Branch_Update", new
                {
                    branch.BranchId,
                    branch.BranchName,
                    branch.Address
                });
            });

            return result > 0;
        }
    }
}

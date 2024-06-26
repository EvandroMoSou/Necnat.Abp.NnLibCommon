﻿using Microsoft.EntityFrameworkCore;
using Necnat.Abp.NnLibCommon.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.Domains.NnIdentity
{
    public class EfCoreNnIdentityRoleRepository : EfCoreRepository<INnLibCommonDbContext, IdentityRole, Guid>, INnIdentityRoleRepository
    {
        public EfCoreNnIdentityRoleRepository(IDbContextProvider<INnLibCommonDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IdentityRole?> FindByNameAsync(string name)
        {
            return await (await GetDbSetAsync()).Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Guid>> GetListIdByNameAsync(List<string> lName)
        {
            return await (await GetDbSetAsync()).Where(x => lName.Contains(x.Name)).Select(x => x.Id).ToListAsync();
        }

        public async Task<string?> FindNameByIdAsync(Guid id)
        {
            return await(await GetDbSetAsync()).Where(x => x.Id == id).Select(x => x.Name).FirstOrDefaultAsync();
        }
    }
}

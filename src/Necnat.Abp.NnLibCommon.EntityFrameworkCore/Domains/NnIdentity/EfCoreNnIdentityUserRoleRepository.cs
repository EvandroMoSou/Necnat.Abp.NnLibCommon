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
    public class EfCoreNnIdentityUserRoleRepository : EfCoreRepository<NnLibCommonDbContext, IdentityUserRole>, INnIdentityUserRoleRepository
    {
        public EfCoreNnIdentityUserRoleRepository(IDbContextProvider<NnLibCommonDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<List<IdentityUserRole>> GetListByUserIdAsync(Guid userId)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}

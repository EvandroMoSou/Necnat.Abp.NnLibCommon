using Microsoft.EntityFrameworkCore;
using Necnat.Abp.NnLibCommon.Domains;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Necnat.Abp.NnLibCommon.EntityFrameworkCore;

[ConnectionStringName(NnLibCommonDbProperties.ConnectionStringName)]
public interface INnLibCommonDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */

    //Identity
    DbSet<IdentityUser> Users { get; }
    DbSet<IdentityRole> Roles { get; }
    DbSet<IdentityClaimType> ClaimTypes { get; }
    DbSet<OrganizationUnit> OrganizationUnits { get; }
    DbSet<IdentitySecurityLog> SecurityLogs { get; }
    DbSet<IdentityLinkUser> LinkUsers { get; }
    DbSet<IdentityUserDelegation> UserDelegations { get; }
    DbSet<IdentityUserRole> UserRoles { get; }

    //Necnat
    DbSet<NnEndpoint> NecnatEndpoint { get; }
}

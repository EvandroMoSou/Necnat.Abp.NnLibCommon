using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Necnat.Abp.NnLibCommon.Dtos
{
    public abstract class ConcurrencyDto : IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyAuditedEntityDto : AuditedEntityDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyAuditedEntityDto<TPrimaryKey> : AuditedEntityDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyAuditedEntityWithUserDto<TPrimaryKey> : AuditedEntityWithUserDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyCreationAuditedEntityDto : CreationAuditedEntityDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyCreationAuditedEntityDto<TPrimaryKey> : CreationAuditedEntityDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyCreationAuditedEntityWithUserDto<TPrimaryKey> : CreationAuditedEntityWithUserDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyEntityDto : EntityDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyEntityDto<TPrimaryKey> : EntityDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleAuditedEntityDto : ExtensibleAuditedEntityDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleAuditedEntityDto<TPrimaryKey> : ExtensibleAuditedEntityDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleAuditedEntityWithUserDto<TPrimaryKey> : ExtensibleAuditedEntityWithUserDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleCreationAuditedEntityDto : ExtensibleCreationAuditedEntityDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleCreationAuditedEntityDto<TPrimaryKey> : ExtensibleCreationAuditedEntityDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleCreationAuditedEntityWithUserDto<TPrimaryKey> : ExtensibleCreationAuditedEntityWithUserDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleEntityDto : ExtensibleEntityDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleEntityDto<TPrimaryKey> : ExtensibleEntityDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleFullAuditedEntityDto : ExtensibleFullAuditedEntityDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleFullAuditedEntityDto<TPrimaryKey> : ExtensibleFullAuditedEntityDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyExtensibleFullAuditedEntityWithUserDto<TPrimaryKey> : ExtensibleFullAuditedEntityWithUserDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyFullAuditedEntityDto : FullAuditedEntityDto, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyFullAuditedEntityDto<TPrimaryKey> : FullAuditedEntityDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }

    public abstract class ConcurrencyFullAuditedEntityWithUserDto<TPrimaryKey> : FullAuditedEntityWithUserDto<TPrimaryKey>, IHasConcurrencyStamp
    {
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }
}

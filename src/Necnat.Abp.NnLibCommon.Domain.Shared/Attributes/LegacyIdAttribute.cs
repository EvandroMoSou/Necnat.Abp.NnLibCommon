using System;

namespace Necnat.Abp.NnLibCommon.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class LegacyIdAttribute : Attribute
    {
        public static readonly LegacyIdAttribute Default = new LegacyIdAttribute();
        private string legacyId;

        public LegacyIdAttribute() : this(string.Empty)
        {
        }

        public LegacyIdAttribute(string legacyId)
        {
            this.legacyId = legacyId;
        }

        public virtual string LegacyId
        {
            get
            {
                return LegacyIdValue;
            }
        }

        protected string LegacyIdValue
        {
            get
            {
                return legacyId;
            }
            set
            {
                legacyId = value;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == this)
            {
                return true;
            }

            LegacyIdAttribute? other = obj as LegacyIdAttribute;

            return (other != null) && other.LegacyId == LegacyId;
        }

        public override int GetHashCode()
        {
            return LegacyId.GetHashCode();
        }
    }
}

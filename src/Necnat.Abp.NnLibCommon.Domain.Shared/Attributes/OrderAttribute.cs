using System;

namespace Necnat.Abp.NnLibCommon.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class OrderAttribute : Attribute
    {
        public static readonly OrderAttribute Default = new OrderAttribute();
        private int order;

        public OrderAttribute() : this(int.MaxValue)
        {
        }

        public OrderAttribute(int order)
        {
            this.order = order;
        }

        public virtual int Order
        {
            get
            {
                return OrderValue;
            }
        }

        protected int OrderValue
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == this)
            {
                return true;
            }

            OrderAttribute? other = obj as OrderAttribute;

            return (other != null) && other.Order == Order;
        }

        public override int GetHashCode()
        {
            return Order.GetHashCode();
        }
    }
}

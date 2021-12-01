using System;

namespace EcommerceApi.Entidades
{
    public abstract class EntidadeBase
    {
        protected EntidadeBase(Guid id)
        {
            ID = id;
        }
        public Guid ID { get; private set; }
    }
}

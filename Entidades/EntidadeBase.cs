using System;

namespace EcommerceApi.Entidades
{
    public abstract class EntidadeBase
    {
        protected EntidadeBase(Guid id)
        {
            ID = id;
        }
        protected EntidadeBase()
        {
            ID= Guid.NewGuid();
        }
        public Guid ID { get; private set; }
    }
}

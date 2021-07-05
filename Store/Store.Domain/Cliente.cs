using System;

namespace Store.Domain
{
    public class Cliente
    {
        public Guid Id { get; private set; }

        public Cliente()
        {
            Id = Guid.NewGuid();
        }
    }
}

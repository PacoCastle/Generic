using Generic.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Generic.Core.Repositories;
using Generic.Core.Models;

namespace Generic.Data.Repositories
{
    class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(DataContext context)
            : base(context)
        { }
    }
}

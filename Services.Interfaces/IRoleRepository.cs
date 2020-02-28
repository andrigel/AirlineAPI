using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IRoleRepository
    {
        public Task SetAsManager(string userId);
        public Task SetAsAdmin(string userId);
        public Task SetAsUser(string userId);
    }
}

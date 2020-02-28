using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Interfaces
{
    public interface IAccountRepository
    {
        public Task Register(RegisterViewModel model);
    }
}

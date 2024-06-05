using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment_API.src.Models.Interfaces
{
    public interface IUnitOfWork
    {
        void Complete();
    }
}
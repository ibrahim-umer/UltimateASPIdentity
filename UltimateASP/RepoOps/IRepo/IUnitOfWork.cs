using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateASP.Data.EntityClasses;

namespace UltimateASP.RepoOps.IRepo
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepo<Country> Countries { get; }
        IGenericRepo<Hotel> Hotels { get; }
        Task Save();
    }
}

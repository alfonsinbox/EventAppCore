using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EventAppCore.Repositories
{
    public interface IRepository<T>
    {
        T GetById(string id);
        IQueryable<T> GetAll();
        IQueryable<T> Search(string query);
        Task<T> Put(T entity);
    }
}
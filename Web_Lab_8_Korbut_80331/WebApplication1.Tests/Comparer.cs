using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Controllers;
using WebApplication1.DAL.Entities;
using Xunit;

namespace WebApplication1.Tests
{
    public class Comparer<T> : IEqualityComparer<T>
    {
        public static Comparer<T> GetComparer(Func<T, T, bool> func)
        {
            return new Comparer<T>(func);
        }
        Func<T, T, bool> comparerFunction;
        public Comparer(Func<T, T, bool> func)
        {
            comparerFunction = func;
        }
        public bool Equals(T x, T y)
        {
            return comparerFunction(x, y);
        }
        public int GetHashCode(T obj)
        {
            throw new NotImplementedException();
        }
    }
}

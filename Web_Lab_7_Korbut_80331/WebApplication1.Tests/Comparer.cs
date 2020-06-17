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
        [Fact]
        public void ControllerSelectsGroup()
        {
            // arrange
            var controller = new ProductController();
            var data = TestData.GetDishesList();
            controller._dishes = data;
            var comparer = Comparer<Dish>
            .GetComparer((d1, d2) => d1.DishId.Equals(d2.DishId));
            // act
            var result = controller.Index(2) as ViewResult;
            var model = result.Model as List<Dish>;
            // assert
            Assert.Equal(2, model.Count);
            Assert.Equal(data[2], model[0], comparer);
        }
    }
}

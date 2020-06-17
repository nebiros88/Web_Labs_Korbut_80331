using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL.Entities;
using WebApplication1.Extensions;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        public List<Dish> _dishes;
        List<DishGroup> _dishGroups;
        int _pageSize;
        public ProductController()
        {
            _pageSize = 3;
            SetupData();
        }

        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo=1)
        {
            // Поместить список групп во ViewData
            var dishesFiltered = _dishes.Where(d => !group.HasValue || d.DishGroupId == group.Value);
            ViewData["Groups"] = _dishGroups;
            // Получить id текущей группы и поместить в TempData
            //var currentGroup = group.HasValue ? group.Value : 0;
            //ViewData["CurrentGroup"] = currentGroup;
            ViewData["CurrentGroup"] = group ?? 0;
            //return View(ListViewModel<Dish>.GetModel(dishesFiltered, pageNo, _pageSize));
            var model = ListViewModel<Dish>.GetModel(dishesFiltered, pageNo, _pageSize);
            if (Request.IsAjaxRequest())
                return PartialView("_listpartial", model);
            else
                return View(model);
        }
        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _dishGroups = new List<DishGroup>
            {
            new DishGroup {DishGroupId=1, GroupName="Стартеры"},
            new DishGroup {DishGroupId=2, GroupName="Салаты"},
            new DishGroup {DishGroupId=3, GroupName="Супы"},
            new DishGroup {DishGroupId=4, GroupName="Основные блюда"},
            new DishGroup {DishGroupId=5, GroupName="Напитки"},
            new DishGroup {DishGroupId=6, GroupName="Десерты"}
            };
            _dishes = new List<Dish>
            {
            new Dish {DishId = 1, DishName="Суп-харчо",
            Description="Очень острый, невкусный",
            Calories =200, DishGroupId=3, Image="Суп.jpg" },
            new Dish { DishId = 2, DishName="Борщ",
            Description="Много сала, без сметаны",
            Calories =330, DishGroupId=3, Image="Борщ.jpg" },
            new Dish { DishId = 3, DishName="Котлета пожарская",
            Description="Хлеб - 80%, Морковь - 20%",
            Calories =635, DishGroupId=4, Image="Котлета.jpg" },
            new Dish { DishId = 4, DishName="Макароны по-флотски",
            Description="С охотничьей колбаской",
            Calories =524, DishGroupId=4, Image="Макароны.jpg" },
            new Dish { DishId = 5, DishName="Компот",
            Description="Быстро растворимый, 2 литра",
            Calories =180, DishGroupId=5, Image="Компот.jpg" }
            };
        }
    }
}

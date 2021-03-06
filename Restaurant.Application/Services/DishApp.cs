﻿using AutoMapper;
using Restaurant.Application.DTO;
using Restaurant.Application.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Application.Services
{
    public class DishApp : ServiceAppBase<Dish, DishDTO>, IDishApp
    {
        public DishApp(IMapper iMapper, IDishService service) : base(iMapper, service)
        {

        }
    }
}

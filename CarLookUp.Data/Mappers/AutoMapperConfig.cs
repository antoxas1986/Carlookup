﻿using AutoMapper;
using CarLookUp.Core.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CarLookUp.Data.Mappers
{
    public static class AutoMapperConfig
    {
        public static void Execute()
        {
            //Data.Mappers.AutoMapperConfig.Execute();
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            LoadCustomMappings(types);
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(ICustomMapper).IsAssignableFrom(t) &&
                        !t.IsAbstract &&
                        !t.IsInterface
                        select (ICustomMapper)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using AutoMapper;

namespace Bakery.MapperConfig
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            Mapper.Configuration.AssertConfigurationIsValid();
            //var config = new MapperConfiguration(cfg => {
            //    cfg.AddProfile<MapperProfile>();
            //});

            ////var mapper = new Mapper(config);
            //var mapper = config.CreateMapper();
        }
    }
}

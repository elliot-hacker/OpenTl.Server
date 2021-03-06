﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace OpenTl.Server.Back
{
    using OpenTl.Server.Back.BLL;
    using OpenTl.Server.Back.BLL.Interfaces;
    using OpenTl.Server.Back.DAL;
    using OpenTl.Server.Back.DAL.Interfaces;
    using OpenTl.Server.Back.Maps;
    using OpenTl.Server.Back.Sessions;
    using OpenTl.Server.Back.Sessions.Interfaces;

    public class ServerStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            GlobalMaps.RegisterMaps();

            services.AddSingleton(typeof(IRepository<>), typeof(MemoryRepository<>));
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ISessionStore, SessionStore>();
            
            return services.BuildServiceProvider();
        }
    }
}
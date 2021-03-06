﻿namespace OpenTl.Server.Back.Maps
{
    using AutoMapper;

    using OpenTl.Schema;
    using OpenTl.Server.Back.Entities;

    using TAuthorization = OpenTl.Schema.Auth.TAuthorization;

    public static class GlobalMaps
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<User, TUser>()
                       .IgnoreAllUnmapped()
                       .ForMember(user => user.FirstName, expression => expression.MapFrom(user => user.FirstName))
                       .ForMember(user => user.LastName, expression => expression.MapFrom(user => user.LastName))
                       .ForMember(user => user.Phone, expression => expression.MapFrom(user => user.PhoneNumber))
                       .ForMember(user => user.Id, expression => expression.MapFrom(user => user.UserId));

                    cfg.CreateMap<User, TAuthorization>()
                       .ForMember(authorization => authorization.User, expression => expression.MapFrom(user => user));
                });
        }
    }
}
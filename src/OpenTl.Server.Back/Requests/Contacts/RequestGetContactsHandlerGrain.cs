﻿namespace OpenTl.Server.Back.Requests.Contacts
{
    using System.Threading.Tasks;

    using AutoMapper;

    using OpenTl.Schema;
    using OpenTl.Schema.Contacts;
    using OpenTl.Server.Back.BLL.Interfaces;
    using OpenTl.Server.Back.Contracts.Requests.Contacts;
    using OpenTl.Server.Back.Entities;
    using OpenTl.Server.Back.Maps;
    using OpenTl.Server.Back.Sessions;
    using OpenTl.Server.Back.Sessions.Interfaces;

    public class RequestGetContactsHandlerGrain : BaseObjectHandlerGrain<RequestGetContacts, IContacts>, IRequestGetContactsHandler
    {
        private readonly IUserService _userService;

        private readonly ISessionStore _sessionStore;

        public RequestGetContactsHandlerGrain(IUserService userService, ISessionStore sessionStore)
        {
            _userService = userService;
            _sessionStore = sessionStore;

            InitMaps();
        }

        private void InitMaps()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.MapEnumerableToVector<int, IContact>();
                    cfg.MapEnumerableToVector<int, IUser>();

                    cfg.CreateMap<int, IContact>()
                       .ForMember(contact => contact.UserId, expression => expression.MapFrom(u => u))
                       .ForMember(contact => contact.Mutual, expression => expression.UseValue(false))
                       .As<TContact>();

                    cfg.CreateMap<int, IUser>()
                    .IgnoreAllUnmapped()
                    .ConstructUsing(userId => Mapper.Map<User, TUser>(_userService.GetById(userId)));

                    cfg.CreateMap<User, TContacts>()
                       .ForMember(contacts => contacts.Contacts, expression => expression.MapFrom(user => user.Contacts))
                       .ForMember(contacts => contacts.Users, expression => expression.MapFrom(user => user.Users));
                });
        }

        protected override Task<IContacts> HandleProtected(ulong clientId, RequestGetContacts obj)
        {
            _sessionStore.TryGetSession(clientId, out var session);

            var currentUser = _userService.GetById(session.CurrentUserId);

            var response = Mapper.Map<TContacts>(currentUser).Cast<IContacts>();

            return Task.FromResult(response);
        }
    }
}
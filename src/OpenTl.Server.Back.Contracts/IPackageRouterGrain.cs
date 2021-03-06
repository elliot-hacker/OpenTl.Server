﻿using System;
using System.Threading.Tasks;

namespace OpenTl.Server.Back.Contracts
{
    public interface IPackageRouterGrain: Orleans.IGrainWithIntegerKey
    {
        Task<byte[]> Handle(ulong clientId, byte[] encryptedRequest);
    }
}
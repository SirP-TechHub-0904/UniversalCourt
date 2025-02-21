﻿using UniversalCourt.Application.Dtos.AwsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalCourt.Application.Repository.DeviceInformation
{
    public interface IDeviceService
    {
        Task<string> GetMackAddress();
        Task<bool> DeviceAccess(string registeredToken);
    }
}

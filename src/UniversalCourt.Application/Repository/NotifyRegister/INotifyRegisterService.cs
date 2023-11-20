using UniversalCourt.Application.Dtos.NotifyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalCourt.Application.Repository.NotifyRegister
{
    public interface INotifyRegisterService
    {
        Task RegisterNotification(NotifyDto obj);

    }
}

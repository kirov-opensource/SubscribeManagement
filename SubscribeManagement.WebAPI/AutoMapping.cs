using AutoMapper;
using SubscribeManagement.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Server, ServerViewModel>();
            CreateMap<CreateServerModel, Server>();
            CreateMap<UpdateServerModel, Server>();
        }
    }
}

using AutoMapper;
using SubscribeManagement.WebAPI.DA.Entities;
using SubscribeManagement.WebAPI.Models.Connection;

namespace SubscribeManagement.WebAPI
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<CreateConnectionModel, Connection>();
        }
    }
}

using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ProjectManager.Services.Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<StatusRequest, Repository.Entities.Status>();
            CreateMap<StatusResponse, StatusRequestUpdate>();
            CreateMap<Repository.Entities.Status, StatusResponse>();
            CreateMap<Repository.Entities.Status, StatusRequestUpdate>();
            CreateMap<StatusRequestUpdate, Repository.Entities.Status>();
            CreateMap<StatusResponse, Repository.Entities.Status>();
        }
    }
}

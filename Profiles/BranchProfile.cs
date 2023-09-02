using System;
using AutoMapper;

namespace SimpleBank.API.Profiles
{
	public class BranchProfile : Profile
	{
		public BranchProfile()
		{
			CreateMap<Entities.Branch, Models.BranchDto>();
            CreateMap<Entities.Branch, Models.BranchWithoutTellerDto>();
        }
    }
}


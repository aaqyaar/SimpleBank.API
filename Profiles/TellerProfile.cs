using System;
using AutoMapper;

namespace SimpleBank.API.Profiles
{
	public class TellerProfile : Profile
	{
		public TellerProfile()
		{
			CreateMap<Entities.Teller, Models.TellerDto>();
		}
	}
}


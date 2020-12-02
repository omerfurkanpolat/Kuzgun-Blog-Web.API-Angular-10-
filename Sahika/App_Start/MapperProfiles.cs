using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Sahika.Dtos;
using Sahika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Sahika.App_Start.IdentityConfig;
using static Sahika.Models.IdentityModels;

namespace Sahika.App_Start
{
    public class MapperProfiles : Profile
    {
       

        public MapperProfiles()
        {
            CreateMap<ApplicationUser, UserForChangeEmailDTO>();            
            CreateMap<PostComment, CommentForReturnDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(i => i.User.UserName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(i => i.UserId))
                .ForMember(dest=>dest.ImageUrl, opt=>opt.MapFrom(i=>i.User.ImageUrl));           
            CreateMap<ApplicationUser, UserForDetailDTO>();
            CreateMap<Category, CategoryForReturnDTO>();
            CreateMap<SubCategory, SubCategoryForReturnDTO>();
            CreateMap<Post, PostForReturnDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(i => i.User.UserName))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(i => i.Category.CategoryName))
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(i => i.SubCategory.SubCategoryName))
                .ForMember(dest => dest.UserImageUrl ,opt => opt.MapFrom(i => i.User.ImageUrl));
            
           CreateMap<SubCategory, SubCategoryForReturnDTO>();
              

        }
    }

    public static class AutoMapperConfing
    {
        public static IMapper _mapper { get; set; }

        public static void Register()
        {
            var mapperConfing = new MapperConfiguration(config =>
            {
                config.AddProfile<MapperProfiles>();
            }
            );
            _mapper = mapperConfing.CreateMapper();            
        }       


    }
}
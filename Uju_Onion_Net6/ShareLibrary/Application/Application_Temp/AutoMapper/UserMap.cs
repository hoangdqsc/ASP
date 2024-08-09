using AutoMapper;
using Core_Temp.Entities;
using Application_Temp.DTOs;

namespace Application_Temp.AutoMapper
{
     public class UserMap : Profile
    {
             
        // Cách 1: phương pháp thủ công
        // ý nghĩa: Bạn định nghĩa cụ thể cách ánh xạ từ UserDto sang User.
        // Điều này hữu ích khi bạn cần kiểm soát chính xác cách ánh xạ diễn ra hoặc khi có các quy tắc ánh xạ khác nhau giữa hai hướng.
        public UserMap()
        {
            // Ánh xạ từ User sang UserDto
            CreateMap<User, UserDto>().ForMember(dest => dest.PasswordHash,
                    opt => opt.MapFrom(src => src.PasswordHash)).ForMember(dest => dest.PasswordHash,
                    opt => opt.MapFrom(src => src.PasswordHash));

            // Ánh xạ ngược từ UserDto sang User
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.MapFrom(src => src.PasswordHash));
        }

     
        /*
         // Cách 2: Sử dụng ReverseMap() sẽ tự động tạo ánh xạ ngược dựa trên cấu hình ban đầu từ User sang UserDto.
         // Đây là cách đơn giản và tiện lợi khi ánh xạ giữa hai đối tượng là tương tự nhau
      public UserMap()
      {
        CreateMap<User, UserDto>().ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.PasswordHash)).ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.PasswordHash)).ReverseMap(); // Tạo ánh xạ ngược tự động từ UserDto sang User
      }

         */
    }
}

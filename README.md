Tạo một lớp cấu hình cho AutoMapper:

using AutoMapper;
using Core_Temp.Entities;
using Application_Temp.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
            .ForMember(dest => dest.MatKhau, opt => opt.MapFrom(src => src.PasswordHash)); // Đổi tên thuộc tính
    }
}

Cấu hình trong Program.cs:
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Thay thế MappingProfile bằng tên lớp cấu hình của bạn

Sử dụng AutoMapper trong UserService:

using AutoMapper;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHelper _passwordHelper;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IPasswordHelper passwordHelper, IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHelper = passwordHelper;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetTaikhoan()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}

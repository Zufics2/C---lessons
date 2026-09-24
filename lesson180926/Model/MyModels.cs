using AutoMapper;

namespace lesson180926.Model
{
    public class MyModels
    {
    }

    public class PostConcatModel
    {
        public string a { get; set; }
        public string b { get; set; }
    }

    public class Model1
    {
        public string a { get; set; }
        public string b { get; set; }
    }

    public class Model2
    {
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Model1, Model2>();
        }
    }

    public class Model3
    {
        public string a { get; set; }
        public string b { get; set; }
    }
    
    public class Model4
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string c { get; set; }
    }

    public class MappingProfile2 : Profile
    {
        public MappingProfile2()
        {
            CreateMap<Model3, Model4>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.a))
                .ForMember(dest => dest.Description,
                    opt => opt.MapFrom(src => src.b));
        }
    }

    public class Model5
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }

    public class Model6
    {
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public string e { get; set; }
        public string f { get; set; }
    }

    public class MappingProfile3 : Profile
    {
        public MappingProfile3()
        {
            CreateMap<Model5, Model6>()
                .ForMember(dest => dest.a,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.b,
                    opt => opt.MapFrom(src => src.BirthDate.ToString("dd.MM.yyyy")))
                .ForMember(dest => dest.c,
                    opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.d,
                    opt => opt.MapFrom(src => src.Description));
        }
    }
}

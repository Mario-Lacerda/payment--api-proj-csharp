using AutoMapper;
using Payment_API.src.Mapping;

namespace Payment_API.Tests
{
    public abstract class MapperConfigurationTest
    {
        protected readonly IMapper _mapper;

        MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new ModelToDtoProfile());
                cfg.AddProfile(new DtoToModelProfile());
            });
        
        public MapperConfigurationTest()
        {
            _mapper = new Mapper(mapperConfig);
        }
    }
}
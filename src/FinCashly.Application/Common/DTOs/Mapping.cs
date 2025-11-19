using AutoMapper;
using FinCashly.Domain.Entities;

namespace FinCashly.Application.Common.DTOs
{
    public class BaseMapping : Profile
    {
        public BaseMapping()
        {
            CreateMap<DateTime, DateTime>().ConvertUsing<UtcToLocalDateTimeConverter>();
        }
    }

    public class UtcToLocalDateTimeConverter : ITypeConverter<DateTime, DateTime>
    {
        public DateTime Convert(DateTime source, DateTime destination, ResolutionContext context)
        {
            return source.ToLocalTime();
        }
    }
}


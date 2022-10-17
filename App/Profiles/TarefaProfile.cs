using App.DTO;
using AutoMapper;
using Dominio.Core.Models.Tarefas;
using System.Reflection;
using System;
using System.Linq;

namespace App.Profiles
{
    public class AutoMapperConfig
    {
        protected static MapperConfiguration GetMapperConfiguration()
        {
            var profiles = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof(Profile).IsAssignableFrom(x));

            return new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
                }
            });
        }
    }
    public class TarefaProfile : Profile
    {
        public TarefaProfile()
        {
            CreateMap<Tarefa, TarefaDto>().ReverseMap();
        }
    }
}

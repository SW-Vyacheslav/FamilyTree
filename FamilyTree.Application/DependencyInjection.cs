using AutoMapper;
using FamilyTree.Application.Common.Behaviours;
using FamilyTree.Application.FamilyTrees.Interfaces;
using FamilyTree.Application.FamilyTrees.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FamilyTree.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IFamilyTreeService, FamilyTreeService>();

            return services;
        }
    }
}

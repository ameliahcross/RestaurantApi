﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StockApp.WebApi.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // mantener los XML de mi proyecto que contienen la documentación
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", searchOption: SearchOption.TopDirectoryOnly).ToList();
                //busca todos los archivos XML del proyecto y se los paso a swagger como comentario
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

                //  Informaciones básicas de la API
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RestaurantApp API",
                    Description = "This Api will be responsible for overall data distribution",
                    Contact = new OpenApiContact // de la persona que creó el API 
                    {
                        Name = "Amelia HC",
                        Email = "ameliahcross@hotmail.com",
                        Url = new Uri("https://www.itla.edu.do")
                    }
                });

                options.DescribeAllParametersInCamelCase();
                // configurar que Swagger trabaje con los tokens
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here}"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
    }
}

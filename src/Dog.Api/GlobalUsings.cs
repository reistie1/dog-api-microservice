global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using Autofac;
global using Autofac.Extensions.DependencyInjection;
global using FluentValidation;
global using MediatR;
global using MediatR.Extensions.Autofac.DependencyInjection;
global using MediatR.Extensions.Autofac.DependencyInjection.Builder;
global using Module = Autofac.Module;
global using Serilog;
global using Splitio.Services.Client.Classes;
global using Dog.Api.Application;
global using Dog.Api.Auth;
global using Dog.Api.Behaviours;
global using Dog.Api.Core;
global using Dog.Api.Core.Errors;
global using Dog.Api.Core.Options;
global using Dog.Api.Core.Models;
global using Dog.Api.Dtos;
global using Dog.Api.Modules;
global using Dog.Api.Infrastructure.Contexts;
global using Dog.Api.Infrastructure.Helpers;


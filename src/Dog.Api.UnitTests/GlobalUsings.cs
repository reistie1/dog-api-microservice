global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using System.Net;
global using System.Security.Claims;
global using Autofac;
global using FluentAssertions;
global using FluentValidation;
global using MediatR;
global using Moq;
global using Xunit;
global using Dog.Api.Application;
global using static Dog.Api.Application.GenerateTokenController;
global using Dog.Api.Auth;
global using Dog.Api.Behaviours;
global using Dog.Api.Core;
global using Dog.Api.Core.Errors;
global using Dog.Api.Core.Models;
global using Dog.Api.Core.Options;
global using Dog.Api.Dtos;
global using Dog.Api.Infrastructure.Contexts;
global using Dog.Api.Infrastructure.Helpers;
global using Dog.Api.Middleware;
global using Dog.Api.Modules;
global using Dog.Api.TestFramework;
global using Dog.Api.TestFramework.Fixtures;
global using Dog.Api.UnitTests.Fixtures;
global using Dog.Api.UnitTests.Mocks;
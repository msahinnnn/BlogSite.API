using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.UserVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Authentication;
using BlogSite.Business.Concrete;
using BlogSite.Business.Constants;
using BlogSite.Business.Validations;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.UserVMs;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Test
{
    public class AuthServiceTest
    {

    }
}

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using IW5.DAL;
using IW5.BL.Models;
using IW5.BL.Models.ModelMappers;
using IW5.Models.Entities;
using FluentAssertions;
using AutoMapper;

namespace IW5.BL.Tests.MapperTests
{
    public abstract class BaseMapperTest
    {
        protected BaseMapperTest()
        {

        }

    }
}
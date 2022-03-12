using System.Diagnostics.CodeAnalysis;
using diegomoreno.Brq.CrossCutting.IoC.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace diegomoreno.Brq.Trucks.Tests.Shared;

[ExcludeFromCodeCoverage]
public static class TestHelper
{
    public static T? GetValue<T>(this IActionResult result) where T : class
    {
        var response = (ResponseDto)result.GetType().GetProperty("Value").GetValue(result);
        return (T) response?.Data;
    }
}
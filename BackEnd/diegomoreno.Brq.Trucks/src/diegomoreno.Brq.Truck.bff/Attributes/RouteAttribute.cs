using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace diegomoreno.Brq.bff.Attributes;

[ExcludeFromCodeCoverage]
public class BrqRouteAttribute : RouteAttribute
{
    public static readonly string BaseRoute = "diegomoreno/brq/bff/v1";

    public BrqRouteAttribute(string template) : base($"{BaseRoute}/{template}") {}
}
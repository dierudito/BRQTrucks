namespace diegomoreno.Brq.bff.Controllers;

public static class Routes
{
    public static class Trucks
    {
        public const string Get = "trucks/{idTruck:guid}";
        public const string GetAll = "trucks";

        public const string Add = "trucks";
        public const string Update = "trucks/{idTruck:guid}";
        public const string Delete = "trucks/{idTruck:guid}";

    }
    public static class Series
    {
        public const string Get = "series/{idSeries:guid}";
        public const string GetAll = "series";

    }
}
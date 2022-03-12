namespace diegomoreno.Brq.bff.Controllers;

public static class Routes
{
    public static class Trucks
    {
        public const string Get = "trucks/{idTruck:guid}";
        public const string GetAll = "trucks";

        public const string Add = "trucks";
        public const string Update = "trucks";
        public const string Delete = "trucks/{idTruck:guid}";

    }
}
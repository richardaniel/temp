using Microsoft.Identity.Client;

namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common.StrategyPais
{
    public class MonedaHondurasStrategy : IMonedaStrategy
    {
        public int GetPaisId() => 1;
        public int GetMonedaID() => 1;

    }

    public class MonedaELSalvadorStrategy : IMonedaStrategy
    {
        public int GetPaisId() => 2;
        public int GetMonedaID() => 2;

    }

    public class MonedaNicaraguaStrategy : IMonedaStrategy
    {
        public int GetPaisId() => 3;
        public int GetMonedaID() => 3;
    }
}

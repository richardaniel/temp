namespace Richar.Academia.ProyectoFinal.WebAPI._Features._Common.StrategyPais
{
    public class MonedaStrategyFactory
    {
        private readonly IConfiguration _configuration;

        public MonedaStrategyFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMonedaStrategy CreateStrategy()
        {
            var pais = _configuration["AppConfig:Pais"];

            return pais switch
            {
                "Honduras" => new MonedaHondurasStrategy(),
                "El Salvador" => new MonedaELSalvadorStrategy(),
                "Nicaragua" => new MonedaNicaraguaStrategy(),
                _ => throw new NotImplementedException()
            };
        }
    }
}

namespace BattleshipMpServer.Strategy
{
    public class Radar
    {
        private readonly RadarStrategyGenerator _strategyGenerator;

        public Radar(RadarStrategyGenerator strategyGenerator)
        {
            _strategyGenerator = strategyGenerator;
        }

        public string ScanAreaWithRandomStrategy(string buttonName)
        {
            var randomStrategy = _strategyGenerator.GenerateRadarStrategyRandomly();

            return randomStrategy.ScanGrid(buttonName);
        }
    }
}
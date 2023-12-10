using System.Collections.Generic;
using System;

namespace BattleshipMpServer.Strategy
{
    public class RadarStrategyGenerator
    {
        private readonly List<IRadarStrategy> _strategies;

        public RadarStrategyGenerator()
        {
            _strategies = new List<IRadarStrategy>
            {
                new DiagonalRadarStrategy(),
                new HorizontalRadarStrategy(),
                new VerticalHorizontalRadarStrategy(),
                new VerticalRadarStrategy()
            };
        }

        public IRadarStrategy GenerateRadarStrategyRandomly()
        {
            var rnd = new Random();

            return _strategies[rnd.Next(_strategies.Count)];
        }
    }
}
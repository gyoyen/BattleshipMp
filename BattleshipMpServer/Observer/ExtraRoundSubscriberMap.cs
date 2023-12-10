using System.Collections.Generic;
using System;
using BattleshipMpServer.Constants;
using System.Linq;

namespace BattleshipMpServer.Observer
{
    public class ExtraRoundSubscriberMap
    {
        public Dictionary<string, ExtraRoundSubscriber> CellMap { get; }
        private readonly Random _rnd;

        public ExtraRoundSubscriberMap()
        {
            CellMap = new Dictionary<string, ExtraRoundSubscriber>();
            _rnd = new Random();

            InitializeCellMap();
        }

        public List<ExtraRoundSubscriber> GetSubscribers()
        {
            return CellMap.Values.ToList();
        }

        public ExtraRoundSubscriber GetExtraRoundSubscriber(string buttonName)
        {
            if (CellMap.ContainsKey(buttonName))
            {
                return CellMap[buttonName];
            }

            return null;
        }

        private void InitializeCellMap()
        {
            for (char column = 'A'; column <= BoardSize.WIDTH_LETTER; column++)
            {
                for (int row = 0; row < BoardSize.HEIGHT; row++)
                {
                    string cell = $"{column}{row}";
                    CellMap[cell] = GetRandomMultiplier();
                }
            }
        }

        private ExtraRoundSubscriber GetRandomMultiplier()
        {
            var choice = _rnd.Next(3);
            switch (choice)
            {
                case 0:
                    return new LowExtraRoundMultiplier();
                case 1:
                    return new MediumExtraRoundMultiplier();
                case 2:
                    return new HighExtraRoundMultiplier();
                default:
                    throw new InvalidOperationException("Invalid random choice for multiplier.");
            }
        }
    }
}

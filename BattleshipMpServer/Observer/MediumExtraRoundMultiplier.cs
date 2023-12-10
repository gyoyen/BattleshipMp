namespace BattleshipMpServer.Observer
{
    public class MediumExtraRoundMultiplier : ExtraRoundSubscriber
    {
        protected override float ExtraRoundChanceMultiplier => 0.1f;
    }
}
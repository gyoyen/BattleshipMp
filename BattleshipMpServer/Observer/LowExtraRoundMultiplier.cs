namespace BattleshipMpServer.Observer
{
    public class LowExtraRoundMultiplier : ExtraRoundSubscriber
    {
        protected override float ExtraRoundChanceMultiplier => 0.05f;
    }
}

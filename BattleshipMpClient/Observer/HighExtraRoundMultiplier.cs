namespace BattleshipMpClient.Observer
{
    public class HighExtraRoundMultiplier : ExtraRoundSubscriber
    {
        protected override float ExtraRoundChanceMultiplier => 0.2f;
    }
}

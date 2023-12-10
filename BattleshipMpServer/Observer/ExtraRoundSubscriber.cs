namespace BattleshipMpServer.Observer
{
    public abstract class ExtraRoundSubscriber
    {
        protected float ExtraRoundChance = 2.0f;

        protected int RoundsInARow = 0;

        public bool Enabled { get; set; } = false;

        protected abstract float ExtraRoundChanceMultiplier { get; }

        public void UpdateRoundsInARow()
        {
            RoundsInARow += 1;
        }

        public void SwitchState()
        {
            Enabled = !Enabled;
        }

        public float GetExtraRoundChancePercentages()
        {
            var chance = ExtraRoundChance + (RoundsInARow * ExtraRoundChanceMultiplier);
            return chance > 100.0f ? 100.0f : chance;
        }
    }
}
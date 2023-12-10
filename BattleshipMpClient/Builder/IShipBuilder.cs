using BattleshipMpClient.Factory.Ship;

namespace BattleshipMp.Builder
{
    public interface IShipBuilder
    {
        IShip CreateSubmarine();

        IShip CreateDestroyer();

        IShip CreateCruiser();

        ISpecialShip CreateBattleship();

        ISpecialShip CreateSpecialSubmarine();

        ISpecialShip CreateSpecialDestroyer();

        ISpecialShip CreateSpecialCruiser();
    }
}
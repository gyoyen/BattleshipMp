using System.Collections.Generic;
using System.Windows.Forms;

namespace BattleshipMpServer.Entity
{
    public abstract class Obsticle
    {
        public List<Control> obsticlePerButton { get; set; }

        public abstract Obsticle ShallowCopy();
        public abstract Obsticle DeepCopy();
    }
}

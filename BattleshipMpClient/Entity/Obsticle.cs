using System.Collections.Generic;
using System.Windows.Forms;

namespace BattleshipMpClient.Entity
{
    public abstract class Obsticle
    {
        public List<Control> obsticlePerButton { get; set; }

        public abstract Obsticle ShallowCopy();
        public abstract Obsticle DeepCopy();
    }
}

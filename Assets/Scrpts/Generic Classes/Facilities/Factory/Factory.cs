using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    public class Factory : Facility, IFactory
    {
        public float ProductGrowth { get; private set; }

        public Factory(Base playerBase) : base (playerBase)
        {
            ProductGrowth = 100f;
        }

        public override bool LevelUp()
        {
            var isEnoughProducts = LevelUpPrice < _base.ProductsCount;
            var isEnoughCredits = LevelUpPrice < _base.CreditsCount;

            var canLevelUp = isEnoughProducts && isEnoughCredits;

            if (!canLevelUp)
            {
                return false;
            }

            _base.AcquireProducts(LevelUpPrice);
            _base.AcquireCredits(LevelUpPrice);

            ProductGrowth *= 1.0175f;
            Level++;

            return true;
        }
    }
}

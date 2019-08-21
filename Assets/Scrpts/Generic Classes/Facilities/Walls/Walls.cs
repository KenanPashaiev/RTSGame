using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    public class Walls : Facility, IWalls
    {
        public float Defence { get; private set; }

        public Walls(Base parentBase) : base (parentBase)
        {
            Level = 1;
            Defence = 1;
        }

        public bool ExpandWalls(int expandCost)
        {
            var minimalLevelToExpand = expandCost + 1;
            if(Level < minimalLevelToExpand)
            {
                return false;
            }

            Level -= expandCost;
            Defence = 1;
            for(int i = 1; i < Level; i++)
            {
                Defence += 0.1f;
            }

            return true;
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

            Defence += 0.1f;
            Level++;

            return true;
        }
    }
}

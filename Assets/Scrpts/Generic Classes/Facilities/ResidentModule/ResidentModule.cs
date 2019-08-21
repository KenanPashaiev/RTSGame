using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    public class ResidentModule : Facility, IResidentModule
    {
        public int AmountOfResidents { get; private set; }
        public int ResidentLimit { get; private set; }
        public int ResidentGrowth { get; private set; }
        
        public ResidentModule(Base parentBase) : base (parentBase)
        {
            AmountOfResidents = 200;
            ResidentLimit = 1000;
            ResidentGrowth = 100;
        }

        public bool AcquireResidents(int amountOfAcquiredResidents)
        {
            if(amountOfAcquiredResidents > AmountOfResidents )
            {
                return false;
            }

            AmountOfResidents -= amountOfAcquiredResidents;

            return true;
        }

        public bool AddResidents(int amountOfAddedResidents)
        { 
            if(amountOfAddedResidents + AmountOfResidents > ResidentLimit)
            {
                amountOfAddedResidents = ResidentLimit - AmountOfResidents;
            }

            AmountOfResidents += amountOfAddedResidents;
            return true;
        }

        public override bool LevelUp()
        {
            var isEnoughProducts = LevelUpPrice < _base.ProductsCount;
            var isEnoughCredits = LevelUpPrice < _base.CreditsCount;

            var canLevelUp = isEnoughProducts && isEnoughCredits;

            if(!canLevelUp)
            {
                return false;
            }

            _base.AcquireProducts(LevelUpPrice);
            _base.AcquireCredits(LevelUpPrice);

            ResidentLimit += 200;
            ResidentGrowth = (int)(ResidentGrowth * 1.05f);
            Level++;

            return true;
        }
    }
}

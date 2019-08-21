using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    public class Portal : Facility
    {
        public float ProductBuyPrice { get; private set; }
        public float ProductSalePrice { get; private set; }

        public float PortalProductGrowthBonus { get; private set; }

        public Portal(Base parentBase) : base(parentBase)
        {
            ProductBuyPrice = 1.5f;
            ProductSalePrice = 0.5f;
            PortalProductGrowthBonus = 1;
        }

        public bool BuyProducts(int boughtProductsCount)
        {
            if(boughtProductsCount > _base.CreditsCount)
            {
                return false;
            }

            _base.AcquireCredits(boughtProductsCount);
            _base.AddProducts(boughtProductsCount / ProductBuyPrice);
            return true;
        }

        public bool SellProducts(int soldProductsCount)
        {
            if(soldProductsCount > _base.ProductsCount)
            {
                return false;
            }

            _base.AcquireProducts(soldProductsCount);
            _base.AddCredits(soldProductsCount * ProductSalePrice);
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
            
            var productPurchaseSellDifference = ProductBuyPrice - ProductSalePrice;
            productPurchaseSellDifference *= 0.99f;

            ProductBuyPrice = 1 + productPurchaseSellDifference / 2;
            ProductSalePrice = 1 - productPurchaseSellDifference / 2;
            PortalProductGrowthBonus += 0.0025f;

            Level++;

            return true;
        }
    }
}

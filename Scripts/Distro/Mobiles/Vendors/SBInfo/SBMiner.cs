using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	public class SBMiner : SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBMiner()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Bag ), 6, Utility.Random( 5, 20 ), 0xE76, 0 ) );
				Add( new GenericBuyInfo( typeof( Candle ), 6, Utility.Random( 5, 20 ), 0xA28, 0 ) );
				Add( new GenericBuyInfo( typeof( Torch ), 7, Utility.Random( 5, 20 ), 0xF6B, 0 ) );
				Add( new GenericBuyInfo( typeof( Lantern ), 2, Utility.Random( 5, 20 ), 0xA25, 0 ) );
				//Add( new GenericBuyInfo( typeof( OilFlask ), 9, Utility.Random( 5, 20 ), 0x####, 0 ) );
				Add( new GenericBuyInfo( typeof( Pickaxe ), 32, Utility.Random( 5, 20 ), 0xE86, 0 ) );
				Add( new GenericBuyInfo( typeof( Shovel ), 12, Utility.Random( 5, 20 ), 0xF39, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Pickaxe ), 16 );
				Add( typeof( Shovel ), 6 );
				Add( typeof( Lantern ), 1 );
				//Add( typeof( OilFlask ), 4 );
				Add( typeof( Torch ), 3 );
				Add( typeof( Bag ), 3 );
				Add( typeof( Candle ), 3 );
			}
		}
	}
}
using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class TokunoFurtrader : Furtrader
	{
		[Constructable]
		public TokunoFurtrader()
		{
		}

		public override VendorShoeType ShoeType { get { return VendorShoeType.NinjaTabi; } }

		public override void InitOutfit()
		{
			AddItem( new SkinningKnife() );
			AddItem( new TattsukeHakama() );
			AddItem( new ClothNinjaJacket() );
		}

		public TokunoFurtrader( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			/*int version = */reader.ReadInt();
		}
	}
}
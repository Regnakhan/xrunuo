using System;
using Server;

namespace Server.Items
{
	public class EssenceOfAchievement : Item, ICommodity
	{
		public override int LabelNumber { get { return 1113325; } } // essence of achievement

		[Constructable]
		public EssenceOfAchievement()
			: this( 1 )
		{
		}

		[Constructable]
		public EssenceOfAchievement( int amount )
			: base( 0x571C )
		{
			Weight = 0.1;
			Stackable = true;
			Hue = 47;
			Amount = amount;
		}

		public EssenceOfAchievement( Serial serial )
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

			/*int version = */
			reader.ReadInt();
		}
	}
}

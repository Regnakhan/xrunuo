using System;
using Server;

namespace Server.Items
{
	public class EssenceOfPassion : Item, ICommodity
	{
		public override int LabelNumber { get { return 1113326; } } // essence of passion

		[Constructable]
		public EssenceOfPassion()
			: this( 1 )
		{
		}

		[Constructable]
		public EssenceOfPassion( int amount )
			: base( 0x571C )
		{
			Weight = 0.1;
			Stackable = true;
			Hue = 1161;
			Amount = amount;
		}

		public EssenceOfPassion( Serial serial )
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

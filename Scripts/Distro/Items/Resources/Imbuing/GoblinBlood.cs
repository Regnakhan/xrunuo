using System;
using Server;

namespace Server.Items
{
	public class GoblinBlood : Item, ICommodity
	{
		public override int LabelNumber { get { return 1113335; } } // goblin blood

		[Constructable]
		public GoblinBlood()
			: this( 1 )
		{
		}

		[Constructable]
		public GoblinBlood( int amount )
			: base( 0x572C )
		{
			Weight = 0.1;
			Stackable = true;
			Amount = amount;
		}

		public GoblinBlood( Serial serial )
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

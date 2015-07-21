using System;
using Server;

namespace Server.Items
{
	public class FrogOrigami : Item
	{
		public override int LabelNumber { get { return 1030298; } } // a delicate origami frog

		[Constructable]
		public FrogOrigami()
			: base( 0x283A )
		{
			LootType = LootType.Blessed;

			Weight = 1.0;
		}

		public FrogOrigami( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			/*int version = */
			reader.ReadInt();
		}
	}
}

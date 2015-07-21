using System;

namespace Server.Items
{
	[Furniture]
	[FlipableAttribute( 0x2DF1, 0x2DF2 )]
	public class RarewoodChest : LockableContainer
	{
		public override int DefaultGumpID { get { return 0x10C; } }
		public override int DefaultDropSound { get { return 0x42; } }

		public override int LabelNumber { get { return 1072863; } } // elven chest

		public override Rectangle2D Bounds
		{
			get { return new Rectangle2D( 80, 5, 140, 70 ); }
		}

		[Constructable]
		public RarewoodChest()
			: base( 0x2DF1 )
		{
			Weight = 1.0;
		}

		public RarewoodChest( Serial serial )
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
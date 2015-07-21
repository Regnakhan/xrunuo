using System;
using Server;

namespace Server.Items
{
	public class UndyingFlesh : Item, ICommodity
	{
		public override int LabelNumber { get { return 1113337; } } // undying flesh

		[Constructable]
		public UndyingFlesh()
			: this( 1 )
		{
		}

		[Constructable]
		public UndyingFlesh( int amount )
			: base( 0x5731 )
		{
			Weight = 0.1;
			Stackable = true;
			Amount = amount;
		}

		public UndyingFlesh( Serial serial )
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

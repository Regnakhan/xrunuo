using System;

namespace Server.Items
{
	public class ParasiticPlant : Item, ICommodity
	{
		[Constructable]
		public ParasiticPlant()
			: this( 1 )
		{
		}

		[Constructable]
		public ParasiticPlant( int amount )
			: base( 0x3190 )
		{
			Stackable = true;
			Weight = 1;
			Amount = amount;
		}

		public ParasiticPlant( Serial serial )
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
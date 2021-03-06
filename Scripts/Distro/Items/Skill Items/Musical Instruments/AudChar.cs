using System;

namespace Server.Items
{
	[Flipable( 0x403B, 0x403C )]
	public class AudChar : BaseInstrument
	{
		[Constructable]
		public AudChar()
			: base( 0x403B, 0x392, 0x44 )
		{
			Weight = 10.0;
		}

		public AudChar( Serial serial )
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

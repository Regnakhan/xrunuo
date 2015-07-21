using System;
using Server;

namespace Server.Items
{
	public class LeafbladeOfEase : Leafblade
	{
		public override int LabelNumber { get { return 1073524; } } // Leafblade of Ease

		[Constructable]
		public LeafbladeOfEase()
		{
			WeaponAttributes.UseBestSkill = 1;
		}


		public LeafbladeOfEase( Serial serial )
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

			/*int version = */reader.ReadInt();
		}
	}
}
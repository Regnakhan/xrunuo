using System;
using Server;

namespace Server.Items
{
	public class CloseHelm : BaseArmor
	{
		public override int BasePhysicalResistance { get { return 3; } }
		public override int BaseFireResistance { get { return 3; } }
		public override int BaseColdResistance { get { return 3; } }
		public override int BasePoisonResistance { get { return 3; } }
		public override int BaseEnergyResistance { get { return 3; } }

		public override int InitMinHits { get { return 45; } }
		public override int InitMaxHits { get { return 60; } }

		public override int StrengthReq { get { return 55; } }

		public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

		[Constructable]
		public CloseHelm()
			: base( 0x1408 )
		{
			Weight = 5.0;
		}

		public CloseHelm( Serial serial )
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

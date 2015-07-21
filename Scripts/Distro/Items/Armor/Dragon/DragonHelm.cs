using System;
using Server.Items;

namespace Server.Items
{
	[Flipable( 0x2645, 0x2646 )]
	public class DragonHelm : BaseArmor
	{
		public override int BasePhysicalResistance { get { return 3; } }
		public override int BaseFireResistance { get { return 3; } }
		public override int BaseColdResistance { get { return 3; } }
		public override int BasePoisonResistance { get { return 3; } }
		public override int BaseEnergyResistance { get { return 3; } }

		public override int InitMinHits { get { return 55; } }
		public override int InitMaxHits { get { return 75; } }

		public override int StrengthReq { get { return 75; } }

		public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }
		public override CraftResource DefaultResource { get { return CraftResource.RedScales; } }

		[Constructable]
		public DragonHelm()
			: base( 0x2645 )
		{
			Weight = 5.0;
		}

		public DragonHelm( Serial serial )
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
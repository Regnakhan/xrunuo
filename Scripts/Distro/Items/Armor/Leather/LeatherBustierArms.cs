using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1c0a, 0x1c0b )]
	public class LeatherBustierArms : BaseArmor
	{
		public override int BasePhysicalResistance { get { return 2; } }
		public override int BaseFireResistance { get { return 4; } }
		public override int BaseColdResistance { get { return 3; } }
		public override int BasePoisonResistance { get { return 3; } }
		public override int BaseEnergyResistance { get { return 3; } }

		public override int InitMinHits { get { return 30; } }
		public override int InitMaxHits { get { return 40; } }

		public override int StrengthReq { get { return 20; } }

		public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource { get { return CraftResource.RegularLeather; } }

		public override bool AllowMaleWearer { get { return false; } }

		[Constructable]
		public LeatherBustierArms()
			: base( 0x1C0A )
		{
			Weight = 1.0;
		}

		public LeatherBustierArms( Serial serial )
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
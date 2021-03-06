﻿using System;
using Server.Items;

namespace Server.Items
{
	public class GargishLeatherChest : BaseArmor
	{
		public override int LabelNumber { get { return 1095329; } } // gargish leather chest

		public override int BasePhysicalResistance { get { return 5; } }
		public override int BaseFireResistance { get { return 8; } }
		public override int BaseColdResistance { get { return 6; } }
		public override int BasePoisonResistance { get { return 6; } }
		public override int BaseEnergyResistance { get { return 7; } }

		public override int InitMinHits { get { return 30; } }
		public override int InitMaxHits { get { return 50; } }

		public override int StrengthReq { get { return 25; } }

		public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource { get { return CraftResource.RegularLeather; } }

		public override Race RequiredRace { get { return Race.Gargoyle; } }

		[Constructable]
		public GargishLeatherChest()
			: this( 0 )
		{
		}

		[Constructable]
		public GargishLeatherChest( int hue )
			: base( 0x404A )
		{
			Layer = Layer.InnerTorso;
			Weight = 8.0;
			Hue = hue;
		}

		public GargishLeatherChest( Serial serial )
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
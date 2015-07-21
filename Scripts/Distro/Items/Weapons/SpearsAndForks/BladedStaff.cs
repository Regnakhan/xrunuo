using System;
using Server.Network;
using Server.Items;
using Server.Engines.Craft;

namespace Server.Items
{
	[Alterable( typeof( DefBlacksmithy ), typeof( DualPointedSpear ) )]
	[FlipableAttribute( 0x26BD, 0x26C7 )]
	public class BladedStaff : BaseSpear
	{
		public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Dismount; } }

		public override int StrengthReq { get { return 40; } }
		public override int MinDamage { get { return 14; } }
		public override int MaxDamage { get { return 16; } }
		public override int Speed { get { return 12; } }

		public override int InitMinHits { get { return 21; } }
		public override int InitMaxHits { get { return 110; } }

		public override SkillName Skill { get { return SkillName.Swords; } }

		[Constructable]
		public BladedStaff()
			: base( 0x26BD )
		{
			Weight = 4.0;
		}

		public BladedStaff( Serial serial )
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

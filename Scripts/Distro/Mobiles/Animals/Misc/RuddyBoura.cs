using System;
using Server;
using Server.Network;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a ruddy boura's corpse" )]
	public class RuddyBoura : BaseCreature, ICarvable
	{
		private bool m_Shorn;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Shorn
		{
			get { return m_Shorn; }
			set { m_Shorn = value; }
		}

		[Constructable]
		public RuddyBoura()
			: base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.3, 0.6 )
		{
			Name = "a ruddy boura";
			Body = 0x2CB;

			SetStr( 330, 420 );
			SetDex( 65, 85 );
			SetInt( 15, 20 );

			SetHits( 400, 515 );

			SetDamage( 16, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.Anatomy, 80.1, 90.0 );
			SetSkill( SkillName.MagicResist, 65.1, 75.1 );
			SetSkill( SkillName.Tactics, 80.1, 90.0 );
			SetSkill( SkillName.Wrestling, 85.1, 100.0 );

			Fame = 2500;
			Karma = -2500;

			Tamable = true;
			MinTameSkill = 17.1;
			ControlSlots = 2;
		}

		public override int GetAngerSound() { return 0x5DF; }
		public override int GetIdleSound() { return 0x5E3; }
		public override int GetAttackSound() { return 0x5E0; }
		public override int GetHurtSound() { return 0x5E2; }
		public override int GetDeathSound() { return 0x5E1; }

		public override int Meat { get { return 10; } }
		public override int Hides { get { return 20; } }
		public override int Blood { get { return 8; } }
		public override int Fur { get { return m_Shorn ? 0 : 30; } }
		public override FurType FurType { get { return FurType.Red; } }
		public override HideType HideType { get { return HideType.Spined; } }
		public override FoodType FavoriteFood { get { return FoodType.FruitsAndVegies; } }
		public override PackInstinct PackInstinct { get { return PackInstinct.Boura; } }

		protected override void OnAfterDeath( Container c )
		{
			base.OnAfterDeath( c );

			if ( !Controlled )
			{
				if ( 0.1 > Utility.RandomDouble() )
					c.DropItem( new BouraSkin() );

				if ( 0.2 > Utility.RandomDouble() )
					c.DropItem( new BouraPelt() );

				if ( 0.002 > Utility.RandomDouble() )
					c.DropItem( new BouraTailShield() );
			}
		}

		public void Carve( Mobile from, Item item )
		{
			if ( m_Shorn )
			{
				// The boura glares at you and will not let you shear its fur.
				PrivateOverheadMessage( MessageType.Regular, 0x3B2, 1112354, from.NetState );
				return;
			}

			Item fur = new RedFur( Fur );

			if ( from.PlaceInBackpack( fur ) )
			{
				from.SendLocalizedMessage( 1112353 ); // You place the gathered boura fur into your backpack.
			}
			else
			{
				from.SendLocalizedMessage( 1112352 ); // You would not be able to place the gathered boura fur in your backpack!
				fur.MoveToWorld( from.Location, from.Map );
			}

			m_Shorn = true;
		}

		public RuddyBoura( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );

			writer.Write( (bool) m_Shorn );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
					{
						m_Shorn = reader.ReadBool();
						break;
					}
			}
		}
	}
}

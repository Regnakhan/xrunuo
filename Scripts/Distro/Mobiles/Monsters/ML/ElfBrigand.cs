using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "an elf corpse" )] 
	public class ElfBrigand : BaseCreature 
	{ 
		public override bool AlwaysMurderer{ get{ return true; } }
		
		[Constructable] 
		public ElfBrigand() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )  // TODO apellweaving AI
		{ 
			// TODO: Funcion de Random Hue para elfos
			Hue = Utility.RandomSkinHue();

			if ( Female = Utility.RandomBool() )
			{
				Body = 606;
				Name = NameList.RandomName( "female" );
			}
			else
			{
				Body = 605;
				Name = NameList.RandomName( "male" );
			}
				
			Title = "the brigand";
			
			SetStr( 86, 100 );
			SetDex( 81, 95 );
			SetInt( 61, 75 );

			SetDamage( 15, 27 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10, 15 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Poison, 10, 15 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );	

			Fame = 1000;
			Karma = -1000;
			
			// outfit
			AddItem( new Shirt( Utility.RandomNeutralHue() ) );
			
			switch( Utility.Random( 4 ) )
			{
				case 0: AddItem( new Sandals() ); break;
				case 1: AddItem( new Shoes() ); break;
				case 2: AddItem( new Boots() ); break;
				case 3: AddItem( new ThighBoots() ); break;
			}
			
			if ( Female )
			{
				if ( Utility.RandomBool() )
					AddItem( new Skirt( Utility.RandomNeutralHue() ) );
				else
					AddItem( new Kilt( Utility.RandomNeutralHue() ) );
			}
			else
				AddItem( new ShortPants( Utility.RandomNeutralHue() ) );

			Utility.AssignRandomHair( this );

			// weapon, shield
			AddItem( Loot.RandomWeapon() );
			
			if ( Utility.RandomBool() )
				AddItem( Loot.RandomShield() );
			
			// loot
			if ( Utility.RandomDouble() < 0.75 )
				PackItem( new SeveredElfEars() );		
								
			PackGold( 50, 150 );
		}

		public ElfBrigand( Serial serial ) : base( serial )
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
			
			/*int version = */reader.ReadInt();
		}
	}
}
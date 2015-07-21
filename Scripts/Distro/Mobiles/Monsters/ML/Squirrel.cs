namespace Server.Mobiles
{
	[CorpseName( "a squirrel corpse" )]
	public class Squirrel : BaseCreature
	{
		[Constructable]
		public Squirrel()
			: base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a squirrel";
			Body = 278;
			BaseSoundID = 0xCC;

			SetStr( 45, 50 );
			SetDex( 35 );
			SetInt( 5 );

			SetHits( 50 );

			SetDamage( 1, 4 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 35, 35 );
			SetResistance( ResistanceType.Poison, 20, 25 );
			SetResistance( ResistanceType.Energy, 20, 25 );

			SetSkill( SkillName.MagicResist, 4 );
			SetSkill( SkillName.Tactics, 4 );
			SetSkill( SkillName.Wrestling, 4 );

			Fame = 300;
			Karma = 0;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 29.1;
		}

		public override FoodType FavoriteFood { get { return FoodType.FruitsAndVegies; } }
		public override int Meat { get { return 1; } }

		public Squirrel( Serial serial )
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

﻿using System;
using System.Xml;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Regions;

namespace Server.Engines.Quests
{
	public class ThievesBeAfootQuest : BaseQuest
	{
		/* Thieves Be Afoot! */
		public override object Title { get { return 1094958; } }

		public override QuestChain ChainID { get { return QuestChain.FlintChain; } }
		public override bool DoneOnce { get { return true; } }
		public override Type NextQuest { get { return typeof( BibliophileQuest ); } }

		/* Travel into the Underworld and search for the stolen barrels of barley.
		 * Return them to Quartermaster Flint for your reward.
		 * 
		 * -----
		 * 
		 * Hail Traveler. Trying to find the fabled Stygian Abyss are ye?
		 * I say good luck, an' be weary for I believe there to be a den o'
		 * thieves hidden in them halls!  Aye, just last night I lost four
		 * barrels o' Barley. I know they be sayin' that none but critters
		 * live in them halls, but I've looked every place I dare and seen
		 * no sign o' me barrels.
		 * 
		 * Hope them lazy Society folk got a good nap last night, cause they
		 * wan have any o' me fine Barley based products unless we get those
		 * barrels back! I canne believe none of them loafers who was guarding
		 * the door saw nothin!  Oy, it makes me so mad, I must not think of
		 * it and control me temper... It's a frickin' barrel of Barley! How
		 * could they miss seeing it???
		 * 
		 * Sorry... I don' mean ta be takin' it out on ye. Tell you what friend.
		 * You find those barrels and I will pay you for bringin' them back.
		 * There be some nasty stuff in thar, so if'n ye bring back all four,
		 * I have somethin' special I will share with ye!
		 */
		public override object Description { get { return 1094960; } }

		/* Fine then, be on yar way but be warned! Ol' Flint makes the best
		 * refreshin' barley products in tha known world! That barley will
		 * not profit ye if'n ol' Flint ha' not prepared it proper!
		 */
		public override object Refuse { get { return 1094961; } }

		/* What? Back so soon and narry a barrel in sight? Be ye advised that
		 * ye are not the only traveler ol' Flint has a lookin fer his barrels!
		 * If'n ye are gonna profit of me potions, ye best be about it!
		 */
		public override object Uncomplete { get { return 1094962; } }

		/* It's good to have ol' Flint's barrels back where they belong. For
		 * this, ye are in for a special treat, some of ol' Flint's special brew!
		 * This brew is so pungent that even after ye sober up people around ye
		 * will start feelin' tipsy! It has a special bottle to keep it contained...
		 * err... I mean fresh, so don't lose it!
		 */
		public override object Complete { get { return 1094966; } }

		public ThievesBeAfootQuest()
		{
			AddObjective( new InternalObjective() );

			AddReward( new BaseReward( typeof( BottleOfPungentBrew ), 1094967 ) ); // a bottle of Flint's Pungent Brew
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

		private class InternalObjective : ObtainObjective
		{
			/* This barrel fits the description Flint gave you. Boy is it heavy! */
			public override int ProgressMessage { get { return 1094964; } }

			public InternalObjective()
				: base( typeof( BarrelOfBarley ), "Barrels of Barley", 4 )
			{
			}
		}
	}

	public class BarrelsOfBarleyChamber : QuestMessageRegion<ThievesBeAfootQuest>
	{
		/* The smug smell of Barley fills this chamber. */
		public override int Message { get { return 1094963; } }

		public BarrelsOfBarleyChamber( XmlElement xml, Map map, Region parent )
			: base( xml, map, parent )
		{
		}
	}

	public class BibliophileQuest : BaseQuest
	{
		/* Bibliophile */
		public override object Title { get { return 1094968; } }

		public override QuestChain ChainID { get { return QuestChain.FlintChain; } }
		public override bool DoneOnce { get { return true; } }

		/* Travel into the Underworld and find the Flint's stolen log book.
		 * Return to Flint with the Log book for your reward.
		 * 
		 * -----
		 * 
		 * Ye will not be believin' what misfortune has befallen me now! No
		 * sooner have I gotten me Barley back, those goblins have gone and
		 * taken me log book! How in the two dimensions am I supposted to
		 * keep up with all of tha supplies with no log book? Of course,
		 * those lay about guards dinna see anything!
		 * 
		 * Listen, ye 've been a blessin' to ol' Flint in tha past so I wanta
		 * make ye another offer. If'n ye will bring ol' Flint's book back ta
		 * 'im, I will give ye a keg o' me special brew!
		 */
		public override object Description { get { return 1094970; } }

		/* 'Tis a fine thing to do to a friend in need!  But so be it. 'Tis not
		 * the first time today ol' Flint has been let down. Won't be the last.
		 */
		public override object Refuse { get { return 1094971; } }

		/* 'Ave ye laid hold to ol' Flint's log book yet? Oh, I'm sorry, I jes'
		 * figured ye would have it back by now... Carry on then!
		 */
		public override object Uncomplete { get { return 1094972; } }

		/* 'Ave ye laid hold to ol' Flint's log book did ye? Let me 'ave a look
		 * here! Bloomin' savages! They dog eared one o' the pages and bent the
		 * corner o' me cover! Blast! Well, that's not fer you to be worryin'
		 * about. Here be yer pay as promised a keg of me brew far yer own. This
		 * keg is special made by me own design, ye can use it to refill that
		 * bottle I gave ye. My brew is too strong for normal bottles, so I hope
		 * ye listened to ol' Flint and kept that bottle!
		 */
		public override object Complete { get { return 1094975; } }

		public BibliophileQuest()
		{
			AddObjective( new InternalObjective() );

			AddReward( new BaseReward( typeof( KegOfPungentBrew ), 1113608 ) ); // a keg of Flint's Pungent Brew
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

		private class InternalObjective : ObtainObjective
		{
			/* This appears to be Flint's logbook. It is not clear why the goblins were
			 * using it in a ritual. Perhaps they were summoning a nefarious intention? */
			public override int ProgressMessage { get { return 1094974; } }

			public InternalObjective()
				: base( typeof( FlintsLogbook ), "Flint's Logbook", 1 )
			{
			}
		}
	}

	public class FlintsLogbookChamber : QuestMessageRegion<BibliophileQuest>
	{
		/* In the center of the room you see an alter. There is a book lying on it. */
		public override int Message { get { return 1094973; } }

		public FlintsLogbookChamber( XmlElement xml, Map map, Region parent )
			: base( xml, map, parent )
		{
		}
	}

	public class QuartermasterFlint : MondainQuester
	{
		private static Type[] m_Quests = new Type[]
			{
				typeof( ThievesBeAfootQuest )
			};

		public override Type[] Quests { get { return m_Quests; } }

		public override void Advertise()
		{
			Say( 1094959 ); // Keep an eye pealed, traveler, thieves be afoot!
		}

		[Constructable]
		public QuartermasterFlint()
			: base( "Quartermaster Flint" )
		{
		}

		public QuartermasterFlint( Serial serial )
			: base( serial )
		{
		}

		public override void InitBody()
		{
			InitStats( 100, 100, 25 );

			CantWalk = true;
			Race = Race.Human;

			Hue = 0x83EA;
			HairItemID = 0x203D;
			HairHue = 0x901;
		}

		public override void InitOutfit()
		{
			AddItem( new Shoes( 0x709 ) );
			AddItem( new LongPants( 0x1BB ) );
			AddItem( new FancyShirt( 0x665 ) );
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
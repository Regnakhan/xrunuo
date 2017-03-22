﻿using System;
using Server;
using Server.Mobiles;

namespace Server.Engines.VendorSearch
{
	public abstract class MagicalAttributeSearchCriterion : ValuedSearchCriterion
	{
		public abstract AosAttribute Attribute { get; }

		public override bool Matches( IVendorSearchItem item )
		{
			return item.Item is IMagicalItem && ( (IMagicalItem) item.Item ).Attributes[Attribute] >= Value;
		}
	}

	public abstract class BooleanMagicalAttributeSearchCriterion : SearchCriterion
	{
		public abstract AosAttribute Attribute { get; }

		public override bool Matches( IVendorSearchItem item )
		{
			return item.Item is IMagicalItem && ( (IMagicalItem) item.Item ).Attributes[Attribute] != 0;
		}
	}

	public class NightSightSearchCriterion : BooleanMagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1015168; } } // Night Sight
		public override AosAttribute Attribute { get { return AosAttribute.NightSight; } }
	}

	public class EnhancePotionsSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060411; } } // enhance potions ~1_val~%
		public override AosAttribute Attribute { get { return AosAttribute.EnhancePotions; } }
	}

	public class LuckSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060436; } } // luck ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.Luck; } }
	}

	public class ReflectPhysicalDamageSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060442; } } // reflect physical damage ~1_val~%
		public override AosAttribute Attribute { get { return AosAttribute.ReflectPhysical; } }
	}

	public class DamageIncreaseSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060401; } } // damage increase ~1_val~%
		public override AosAttribute Attribute { get { return AosAttribute.WeaponDamage; } }
	}

	public class DefenseIncreaseChanceSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060408; } } // defense chance increase ~1_val~%
		public override AosAttribute Attribute { get { return AosAttribute.DefendChance; } }
	}

	public class HitChanceIncreaseSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060415; } } // hit chance increase ~1_val~%
		public override AosAttribute Attribute { get { return AosAttribute.AttackChance; } }
	}

	public class SwingSpeedIncreaseSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060486; } } // swing speed increase ~1_val~%
		public override AosAttribute Attribute { get { return AosAttribute.WeaponSpeed; } }
	}

	public class SpellDamageIncreaseSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060483; } } // spell damage increase ~1_val~%
		public override AosAttribute Attribute { get { return AosAttribute.SpellDamage; } }
	}

	public class CastingFocusSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1113696; } } // Casting Focus ~1_val~%
		public override AosAttribute Attribute { get { return AosAttribute.CastingFocus; } }
	}

	public class FasterCastRecoverySearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060412; } } // faster cast recovery ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.CastRecovery; } }
	}

	public class FasterCastingSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060413; } } // faster casting ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.CastSpeed; } }

		public override bool Matches( IVendorSearchItem item )
		{
			var magicalItem = item.Item as IMagicalItem;

			if ( magicalItem == null )
				return false;

			int value = Value;

			if ( magicalItem.Attributes.SpellChanneling != 0 )
				value++;

			return magicalItem.Attributes[Attribute] >= value;
		}
	}

	public class LowerManaCostSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060433; } } // lower mana cost ~1_val~%
		public override AosAttribute Attribute { get { return AosAttribute.LowerManaCost; } }
	}

	public class LowerReagentCostSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060434; } } // lower reagent cost ~1_val~%
		public override AosAttribute Attribute { get { return AosAttribute.LowerRegCost; } }
	}

	public class SpellChannelingCostSearchCriterion : BooleanMagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1079766; } } // Spell Channeling
		public override AosAttribute Attribute { get { return AosAttribute.SpellChanneling; } }
	}

	public class StrengthBonusSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060485; } } // strength bonus ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.BonusStr; } }
	}

	public class DexterityBonusSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060409; } } // dexterity bonus ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.BonusDex; } }
	}

	public class IntelligenceBonusSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060432; } } // intelligence bonus ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.BonusInt; } }
	}

	public class HitPointsIncreaseSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060431; } } // hit point increase ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.BonusHits; } }
	}

	public class StaminaIncreaseSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060484; } } // stamina increase ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.BonusStam; } }
	}

	public class ManaIncreaseSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060439; } } // mana increase ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.BonusMana; } }
	}

	public class HitPointRegenerationSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060444; } } // hit point regeneration ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.RegenHits; } }
	}

	public class StaminaRegerationSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060443; } } // stamina regeneration ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.RegenStam; } }
	}

	public class ManaRegenerationSearchCriterion : MagicalAttributeSearchCriterion
	{
		public override int LabelNumber { get { return 1060440; } } // mana regeneration ~1_val~
		public override AosAttribute Attribute { get { return AosAttribute.RegenMana; } }
	}
}

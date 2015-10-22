using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemGiver : MonoSingleton<ItemGiver> {

    [SerializeField]
    private Item.ItemType[] DistribRank1;
    [SerializeField]
    private Item.ItemType[] DistribRank2;
    [SerializeField]
    private Item.ItemType[] DistribRank3;
    [SerializeField]
    private Item.ItemType[] DistribRank4;
    [SerializeField]
    private Item.ItemType[] DistribRank5;
    [SerializeField]
    private Item.ItemType[] DistribRank6;
    [SerializeField]
    private Item.ItemType[] DistribRank7;
    [SerializeField]
    private Item.ItemType[] DistribRank8;

    private Item.ItemType[][] distribs;

	// Use this for initialization
	void Start () {
        distribs = new Item.ItemType[][]{DistribRank1, DistribRank2, DistribRank3, DistribRank4, DistribRank5, DistribRank6, DistribRank7, DistribRank8};

        for (int ite = 0; ite < 8; ++ite)
        {
            if (distribs[ite].Length == 0)
            {
                Debug.Log("Please set an item distrib for rank " + (ite+1).ToString() + " in the ItemGiver of this level.");
            }
        }
	}

    public Item BalancedRandomItem(int rank)
    {
        if (rank > distribs.Length)
        {
            Debug.Log("Rank too high for Item Giver ! Rank treated as last handled.");
            rank = distribs.Length;
        }

        Item.ItemType[] rankDistrib = distribs[rank - 1];

        if (rankDistrib.Length == 0)
        {
            Debug.Log("No item to give to rank " + rank.ToString() + ".");
            return null;
        }

        int randIterator = Random.Range(0, rankDistrib.Length);

        switch (rankDistrib[randIterator])
        {
            case Item.ItemType.Mushroom:
                return new ItemMushroom();
            case Item.ItemType.TripleMushroom:
                return new ItemTripleMushroom();
            case Item.ItemType.GreenShell:
                return new ItemGreenShell();
            case Item.ItemType.TripleGreenShell:
                return new ItemTripleGreenShell();
            case Item.ItemType.RedShell:
                return new ItemRedShell();
            case Item.ItemType.TripleRedShell:
                return new ItemTripleRedShell();
            case Item.ItemType.BobOmb:
                return new ItemBobOmb();
            case Item.ItemType.Banana:
                return new ItemBanana();
            case Item.ItemType.TripleBanana:
                return new ItemTripleBanana();
            case Item.ItemType.TrappedCube:
                return new ItemTrappedCube();
            case Item.ItemType.Star:
                return new ItemStar();
            default:
                Debug.Log("Unrecognized item to give !");
                return null;
        }
    }
}

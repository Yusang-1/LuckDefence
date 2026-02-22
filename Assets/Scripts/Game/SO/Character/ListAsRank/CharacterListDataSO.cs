using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CharacterListDataSO", menuName = "Scriptable Objects/CharacterListDataSO")]
public class CharacterListDataSO : ScriptableObject
{
    [SerializeField] private CharListAsRank[] charListAsRanks;
    private Dictionary<CharRank, CharListAsRank> charListAsRankDictionary;
    [SerializeField] int codeUnit;
    public Dictionary<CharRank, CharListAsRank> CharListAsRankDictionary => charListAsRankDictionary;

    public void Initialize()
    {
        charListAsRankDictionary = new Dictionary<CharRank, CharListAsRank>();

        for (int i = 0; i < charListAsRanks.Length; i++)
        {
            charListAsRankDictionary.Add(charListAsRanks[i].Rank, charListAsRanks[i]);

            charListAsRanks[i].Initialize();
        }
    }

    public CharRank GetCharRankByCode(int code)
    {
        return (CharRank)(code / codeUnit - 1);
    }
}

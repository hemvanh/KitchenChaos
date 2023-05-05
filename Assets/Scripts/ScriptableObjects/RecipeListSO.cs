using System.Collections.Generic;
using UnityEngine;

// can disable Create asset menu after 1 being created
//[CreateAssetMenu]

// This pattern can make reference to list of ScriptableObject
// with less redundancy and code duplicated, and ensure the integrity
// of all SO List accross all refs
public class RecipeListSO : ScriptableObject {
    public List<RecipeSO> recipeSOList;
}

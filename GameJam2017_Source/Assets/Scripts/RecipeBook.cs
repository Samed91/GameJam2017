using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string name;
    public List<Item> ingredients = new List<Item>();
}

public class RecipeBook : MonoBehaviour
{
    public List<Recipe> recipes = new List<Recipe>();

    public Potion GetCraftedPotion(List<Item> items)
    {
        string currentName = "Unknown Potion";
        GameObject nullObJect = Resources.Load("Stock/" + currentName) as GameObject;
        foreach (Recipe r in recipes)
        {
            currentName = r.name;
            bool hasIngredients = false;
            for (int i = 0; i < r.ingredients.Count; ++i)
            {
                for (int j = 0; j < items.Count; ++j)
                {
                    if (items[j].itemName == r.ingredients[i].itemName)
                    {
                        hasIngredients = true;
                        break;
                    }
                    else
                    {
                        hasIngredients = false;
                        if (j == items.Count)
                            break;
                    }
                }
            }

            if (hasIngredients)
            {
                GameObject temp = Resources.Load("Stock/" + currentName) as GameObject;
                return temp.GetComponent<Potion>();
            }

        }
        return nullObJect.GetComponent<Potion>();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultZone : Zone < ResultZone > {

    #region Spider Solitaire Condition

    public bool IsFullCards()
    {
        // TODO: Get the total cards from first id.
        int totalCards = GetTotalCards(Enums.IdTransformCard.TransformCards_A);        

        // TODO: Return the value.
        return totalCards >= Contains.TotalCardsAreUsing;
    }

    #endregion

}

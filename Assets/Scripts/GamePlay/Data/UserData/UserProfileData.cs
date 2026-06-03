using Newtonsoft.Json;
using UnityEngine;

public class UserProfileData
{
    public const int DEFAULT_CAMPAIGN_STAGE_ID = 1;
    public const int MAX_CAMPAIGN_STAGE_ID = 100;

    public int currentStageId { get; set; } = DEFAULT_CAMPAIGN_STAGE_ID;

    #region Stage
    public void EndStage(bool isWin)
    {
        if (isWin)
        {
            if (currentStageId < MAX_CAMPAIGN_STAGE_ID)
            {
                currentStageId++;
            }
            else
            {
                currentStageId = MAX_CAMPAIGN_STAGE_ID;
            }
        }

        //GameData.userData.Save();
    }

    #endregion
}

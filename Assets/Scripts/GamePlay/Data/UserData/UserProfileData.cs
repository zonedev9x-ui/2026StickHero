using Newtonsoft.Json;
using UnityEngine;

public class UserProfileData : BaseUserData
{
    public const int DEFAULT_CAMPAIGN_STAGE_ID = 1;
    public const int MAX_CAMPAIGN_STAGE_ID = 3;

    public int currentStageId { get; set; } = DEFAULT_CAMPAIGN_STAGE_ID;

    public bool passed { get; set; }

    protected override string GetDataKey()
    {
        return UserData.DATA_KEY_PROFILE;
    }

    public override void ValidateData()
    {
        if (currentStageId < DEFAULT_CAMPAIGN_STAGE_ID)
        {
            currentStageId = DEFAULT_CAMPAIGN_STAGE_ID;
            isDataChanged = true;
        }
        else if (currentStageId > MAX_CAMPAIGN_STAGE_ID)
        {
            currentStageId = MAX_CAMPAIGN_STAGE_ID;
            passed = true;
            isDataChanged = true;
        }
    }

    #region Stage
    public void EndStage(bool isWin)
    {
        if (isWin)
        {
            if (currentStageId < MAX_CAMPAIGN_STAGE_ID)
            {
                currentStageId++;
                passed = false;
            }
            else
            {
                currentStageId = MAX_CAMPAIGN_STAGE_ID;
                passed = true;
            }
        }
        else
        {
            passed = false;
        }

        GameData.userData.Save();
    }

    #endregion
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public const string DATA_KEY_PROFILE = "key_user_profile";

    public UserProfileData profile;

    public bool isDataReplaced;
    public bool isDisableSaveDataLocal;

    private List<BaseUserData> listData;
    private float lastTimeSaveData;

    [JsonIgnore] public bool isNewUser { get; private set; }

    #region Data

    public void ValidateData()
    {
        if (listData == null)
        {
            listData = new List<BaseUserData>();
            listData.Add(profile);
        }

        for (int i = 0; i < listData.Count; i++)
        {
            try
            {
                listData[i].ValidateData();
            }
            catch (Exception e)
            {
                DebugCustom.LogError(e.Message);
                DebugCustom.LogFormat("i={0}, data={1}", i, JsonConvert.SerializeObject(listData[i]));
                continue;
            }
        }
    }

    public void Save(bool isForceSave = true)
    {
        if (isDisableSaveDataLocal) return;

        float interval = 1f;

#if UNITY_EDITOR
        interval = 1f;
#endif
        if (isForceSave || Time.realtimeSinceStartup - lastTimeSaveData > interval)
        {
            lastTimeSaveData = Time.realtimeSinceStartup;
            bool savePrefs = false;

            for(int i = 0; i < listData.Count; i++)
            {
                BaseUserData data = listData[i];

                if (data.Save(isForceSave))
                {
                    savePrefs = true;
                }
            }

            if (savePrefs)
            {
                PlayerPrefs.Save();
            }
        }
    }

    #endregion

    #region Load

    public void Load()
    {
        if(isDataReplaced == false)
        {
            LoadProfile();
        }

        ValidateData();
        isDisableSaveDataLocal = false;

        if (isDataReplaced)
        {
            Save(true);
            isDataReplaced = false;
        }
    }

    private void LoadProfile()
    {
        string prefs = PlayerPrefs.GetString(DATA_KEY_PROFILE);

        if (string.IsNullOrEmpty(prefs))
        {
            profile = new UserProfileData();
            isNewUser = true;
        }
        else
        {
            try
            {
                profile = JsonConvert.DeserializeObject<UserProfileData>(prefs);
            }
            catch
            {
                profile = new UserProfileData();
                isNewUser = true;

                DebugCustom.LogError("LoadProfile");
            }
        }

        DebugCustom.Log("profile=" + JsonConvert.SerializeObject(profile));
    }

    #endregion
}

using Newtonsoft.Json;
using UnityEngine;

public class BaseUserData
{
    [JsonIgnore] public bool isDataChanged;

    protected virtual string GetDataKey()
    {
        return string.Empty;
    }

    public virtual bool Save(bool forceSave)
    {   
        if(forceSave == true)
        {
            isDataChanged = true;
        }

        if(isDataChanged == true)
        {
            PlayerPrefs.SetString(GetDataKey(), JsonConvert.SerializeObject(this));
            isDataChanged = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void ValidateData() { }
}

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SlotData))]
public class SlotDataDrawer : PropertyDrawer
{
    // Cài đặt độ cao của từng phần tử trên Inspector
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty typeProp = property.FindPropertyRelative("slotType");
        
        // Nếu không phải là Enemy/Weapon/Obstacle thì chỉ cao 1 dòng (cho enum SlotType)
        if (typeProp.enumValueIndex == (int)SlotType.None)
        {
            return EditorGUIUtility.singleLineHeight;
        }
        
        // Nếu có chọn type khác None, thì sẽ tốn 2 dòng (1 cho type, 1 cho data)
        return EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Bắt đầu vẽ Property
        EditorGUI.BeginProperty(position, label, property);

        // Tìm các Property con bên trong SlotData
        SerializedProperty typeProp = property.FindPropertyRelative("slotType");
        SerializedProperty enemyProp = property.FindPropertyRelative("enemyData");
        // SerializedProperty weaponProp = property.FindPropertyRelative("weaponData"); 
        // SerializedProperty obstacleProp = property.FindPropertyRelative("obstacleData");

        // Tính toán Rect dòng đầu tiên để vẽ SlotType
        Rect typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        
        // Vẽ Enum Popup cho SlotType
        EditorGUI.PropertyField(typeRect, typeProp, new GUIContent("Type"));

        // Lấy type hiện tại đang được chọn
        SlotType currentType = (SlotType)typeProp.enumValueIndex;

        // Tính toán Rect cho dòng thứ 2 (để vẽ Data tương ứng)
        Rect dataRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);

        // Chỉ vẽ field Data nếu Type tương ứng được chọn
        // Thụt lề vào một chút cho đẹp
        EditorGUI.indentLevel++;
        
        switch (currentType)
        {
            case SlotType.Enemy:
                EditorGUI.PropertyField(dataRect, enemyProp, new GUIContent("Enemy Data"));
                break;
            case SlotType.Weapon:
                // Nếu sau này bạn mở comment weaponData trong SlotData.cs thì dùng dòng dưới:
                // EditorGUI.PropertyField(dataRect, weaponProp, new GUIContent("Weapon Data"));
                GUI.Label(dataRect, "Weapon Data (Chưa được bật trong SlotData.cs)");
                break;
            case SlotType.Obstacle:
                // Tương tự cho obstacle
                // EditorGUI.PropertyField(dataRect, obstacleProp, new GUIContent("Obstacle Data"));
                GUI.Label(dataRect, "Obstacle Data (Chưa được bật trong SlotData.cs)");
                break;
        }
        
        EditorGUI.indentLevel--;

        // Kết thúc vẽ
        EditorGUI.EndProperty();
    }
}

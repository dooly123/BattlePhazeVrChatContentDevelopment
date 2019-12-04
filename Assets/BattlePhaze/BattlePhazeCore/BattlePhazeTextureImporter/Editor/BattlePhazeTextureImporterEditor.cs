using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace BattlePhaze.TextureImport.Handler.EditorCustom
{
    /// <summary>
    /// Editor for texture mass reimporting with appropriated settings
    /// </summary>
    [CustomEditor(typeof(BattlePhazeTextureImporter))]
    public class BattlePhazeTextureImporterEditor : Editor
    {
        public Color32 PrimaryWhiteColor = new Color32(242, 85, 228, 255);
        public Color32 SecondaryColor = new Color32(140, 140, 140, 255);
        public BattlePhazeTextureImporter BattlePhazeTextureManager;
        public GUIStyle StyleButton;
        public GUIStyle TextStyling;
        public GUIStyle EnumStyling;
        public bool rerun = false;
        public override void OnInspectorGUI()
        {
            BattlePhazeTextureManager = (BattlePhazeTextureImporter)target;
            if (rerun == false)
            {
                UIDesign();
            }
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Wrap mode", TextStyling);
            BattlePhazeTextureManager.ModifyWrapMode = EditorGUILayout.Toggle(BattlePhazeTextureManager.ModifyWrapMode);
            GUILayout.EndVertical();
            if (BattlePhazeTextureManager.ModifyWrapMode)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Video Format", TextStyling);
                BattlePhazeTextureManager.WrapMode = (TextureWrapMode)EditorGUILayout.EnumPopup(BattlePhazeTextureManager.WrapMode, EnumStyling);
                GUILayout.EndVertical();
            }
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Texture Type", TextStyling);
            BattlePhazeTextureManager.ModifyTextureType = EditorGUILayout.Toggle(BattlePhazeTextureManager.ModifyTextureType);
            GUILayout.EndVertical();
            if (BattlePhazeTextureManager.ModifyTextureType)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Max texture Size", TextStyling);
                BattlePhazeTextureManager.TextureType = (TextureImporterType)EditorGUILayout.EnumPopup(BattlePhazeTextureManager.TextureType, EnumStyling);
                GUILayout.EndVertical();
            }
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Max texture Size", TextStyling);
            BattlePhazeTextureManager.ModifyMaxTextureSize = EditorGUILayout.Toggle(BattlePhazeTextureManager.ModifyMaxTextureSize);
            GUILayout.EndVertical();
            if (BattlePhazeTextureManager.ModifyMaxTextureSize)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Texture Import Type", TextStyling);
                BattlePhazeTextureManager.TextureSize = (BattlePhazeTextureImporter.MaxTextureSize)EditorGUILayout.EnumPopup(BattlePhazeTextureManager.TextureSize, EnumStyling);
                GUILayout.EndVertical();
            }
            if (GUILayout.Button("Convert Selected", StyleButton))
            {
                EditorUtility.SetDirty(BattlePhazeTextureManager);
            Object[] objects =   Selection.objects;
                for (int ObjectIndex = 0; ObjectIndex < objects.Length; ObjectIndex++)
                {
                    BattlePhazeTextureManager.TexturePropertyModification(AssetDatabase.GetAssetPath(objects[ObjectIndex]));
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
        /// <summary>
        /// UI related stuff
        /// </summary>
        public void UIDesign()
        {
            rerun = true;
            StyleButton = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                margin = new RectOffset(3, 3, 3, 3),
                fontSize = 15,
                fontStyle = FontStyle.Bold
            };
            StyleButton.normal.background = SetColor(40, 40, SecondaryColor);
            StyleButton.normal.textColor = PrimaryWhiteColor;
            TextStyling = new GUIStyle(GUI.skin.textArea)
            {
                alignment = TextAnchor.MiddleLeft,
                margin = new RectOffset(3, 3, 3, 3),
                fontSize = 15,
                fontStyle = FontStyle.Bold
            };
            TextStyling.fixedWidth = 300;
            TextStyling.fixedHeight = 20;
            TextStyling.normal.background = SetColor(2, 2, new Color(0.9f, 0.9f, 0.9f, 0f));
            TextStyling.clipping = TextClipping.Overflow;
            TextStyling.normal.textColor = PrimaryWhiteColor;
            EnumStyling = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 10,
                fontStyle = FontStyle.Bold
            };
            EnumStyling.normal.background = SetColor(2, 2, new Color(0.4f, 0.4f, 0.4f, 0.5f));
            EnumStyling.normal.textColor = PrimaryWhiteColor;
        }
        /// <summary>
        /// Used for setting a color of a Texture2D
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public Texture2D SetColor(int x, int y, Color32 color)
        {
            Texture2D tex = new Texture2D(x, y);
            Color[] pix = new Color[x * y];

            for (int i = 0; i < pix.Length; i++)
            {
                pix[i] = color;
            }
            tex.SetPixels(pix);
            tex.Apply();
            return tex;
        }
    }
}

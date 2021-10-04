#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Globalization;
using System;
using Unity.Collections;

/// <summary>
/// Katsumasa.Kimura
/// </summary>
namespace UTJ
{    
    public partial class EditorGUILayoutExt
    {
        /// <summary>
        /// グラフの描画を行う
        /// </summary>
        /// <param name="content">GUIContent</param>
        /// <param name="list">グラフのデータ</param>
        /// <param name="color">グラフの色</param>
        /// <param name="height"></param>
        static public void Graph (GUIContent content, List<float> list, Color color,float height = 50.0f)
        {

            if (content != null)
            {
                UnityEditor.EditorGUILayout.LabelField(content);
            }
            var area = GUILayoutUtility.GetRect(Mathf.Min(EditorGUIUtility.currentViewWidth, 300f), height);
            EditorGUI.DrawRect(area, UnityEngine.Color.gray);


            if (list.Count != 0)
            {
                var maxValue = list.Max();
                var avgValue = list.Average();
                var scale = area.height / maxValue * 0.90f; // 最大値の高さが描画範囲の80%位に

                for (var i = 0; i < list.Count; i++)
                {
                    var w = 1.0f;
                    var h = list[list.Count - (i + 1)] * scale;
                    var x = area.x + area.width - (i + 1) * w;
                    var y = area.y + area.height;
                    var rect = new Rect(x, y, w, -h);
                    EditorGUI.DrawRect(rect, color);
                }

                // 最大値の補助線
                {
                    var x = area.x;
                    var y = area.y + area.height - maxValue * scale;
                    var w = area.width;
                    var h = 1.0f;
                    EditorGUI.DrawRect(new Rect(x, y, w, h),Color.white);
                    var label = new GUIContent(Format("{0,3:F1}", maxValue));
                    var contentSize = EditorStyles.label.CalcSize(label);
                    var rect = new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y);
                    EditorGUI.DrawRect(rect, Color.black);
                    EditorGUI.LabelField(rect, label);
                }

                // 平均値の補助線
                {
                    var x = area.x;
                    var y = area.y + area.height - avgValue * scale;
                    var w = area.width;
                    var h = 1.0f;
                    EditorGUI.DrawRect(new Rect(x, y, w, h),Color.white);
                    var label = new GUIContent(Format("{0,3:F1}", avgValue));
                    var contentSize = EditorStyles.label.CalcSize(label);
                    var rect = new Rect(x, y - contentSize.y / 2, contentSize.x, contentSize.y);
                    EditorGUI.DrawRect(rect, Color.black);
                    EditorGUI.LabelField(rect, label);                    
                }
            }
        }


        static string Format(string fmt, params object[] args)
        {
            return String.Format(CultureInfo.InvariantCulture.NumberFormat, fmt, args);

        }
    }
}
#endif

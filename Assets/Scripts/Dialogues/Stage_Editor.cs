using UnityEditor;
using UnityEngine;

namespace Dialogue_Helper
{
    [CustomEditor(typeof(Stage))]
    public class Stage_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Read"))
            {

            }
            DrawDefaultInspector();
                //add everthing the button would do.
         }
    }
}
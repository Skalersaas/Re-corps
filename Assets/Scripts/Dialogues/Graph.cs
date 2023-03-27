using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
namespace Dialogue_Helper
{
    public class Graph : EditorWindow
    {
        private Graphview m_Graphview;
        private Data_Holder m_Holder;
        [MenuItem("Graph/Flowchart")]
        public static void New()
        {
            GetWindow<Graph>("Flowchart");
        }
        private Toolbar ToolBar()
        {
            var tools = new Toolbar();
            tools.Add(new Button(() => m_Graphview.New_Node(node.Type.Message)) { text = "New Dialogue" });
            tools.Add(new Button(() => m_Graphview.New_Node(node.Type.If)) { text = "New IF" });
            tools.Add(new Button(() => m_Graphview.New_Group()) { text = "New Group" });
            tools.Add(new Button(() => Save()) { text = "Save" });
            return tools;
        }
        private void OnSelectionChange()
        {
            if (Selection.activeObject != null)
                if (Selection.activeObject.GetType().Equals(typeof(Data_Holder)))
                    Read((Data_Holder)Selection.activeObject);
        }
        private void OnEnable()
        {
            rootVisualElement.Clear();
            Label l = new Label()
            {
                text = "Select Object to read/write"
            };
            Button b = new Button(() => Debug.Log("S"))
            {
                text = "Create new Object"
            };
            rootVisualElement.Add(l);
            rootVisualElement.Add(b);
        }
        private void Read(Data_Holder obj)
        {
            m_Holder = obj;
            m_Graphview = obj.Read();
            rootVisualElement.Clear();
            rootVisualElement.Add(m_Graphview);
            m_Graphview.StretchToParentSize();
            rootVisualElement.Add(ToolBar());
        }
        private void Save()
        {
            m_Holder.Save(m_Graphview);
        }
    }
}
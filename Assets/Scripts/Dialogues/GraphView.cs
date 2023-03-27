using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
namespace Dialogue_Helper
{
    public class Graphview : GraphView
    {
        public Graphview(bool unsaved = true)
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            styleSheets.Add(Resources.Load<StyleSheet>("Grid"));
            var grid = new GridBackground();
            grid.StretchToParentSize();
            Insert(0, grid);
            if(unsaved)
            New_Node(node.Type.Entry);
        }
        public void New_Node(node.Type tp)
        {
            var node = new node(tp);
            AddElement(node);
        }
        public void New_Group()
        {
            var Group = new group();
            AddElement(Group);
        }
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            ports.ForEach((port) =>
            {
                if (startPort.node != port.node && startPort.direction != port.direction)
                    compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }
    }
}
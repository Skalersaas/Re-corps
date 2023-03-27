using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Dialogue_Helper
{
    [CreateAssetMenu(fileName = "Data_Holder", menuName = "ScriptableObjects/Data_Holder", order = 1)]
    public class Data_Holder : ScriptableObject
    {
        [SerializeField] List<Node_data> Nodes = new();
        public void Save(Graphview graph)
        {
            Nodes.Clear();
            foreach (var node in graph.nodes.Cast<node>())
            {
                Nodes.Add(new(node));
            }
        }
        public Graphview Read()
        {
            Graphview graph = new(false);
            foreach (var node in Nodes) graph.AddElement(node.To_Node());
            foreach (var node in graph.nodes.Cast<node>())
            {
                for (int i = 0; i < node.info.Count; i++)
                {
                    if (node.info[i].next_GUID != string.Empty)
                    {
                        Debug.Log(node.title);
                        var Port = (Port)node.outputContainer[i];
                        var Node = from nod in graph.nodes.Cast<node>()
                                   where nod.M_info.GUID == node.info[i].next_GUID
                                   select nod;
                        if (Node.Count() > 0)
                        {
                            Debug.Log(Node.ElementAt(0).name);
                            var edge = Port.ConnectTo((Port)Node.ElementAt(0).inputContainer[0]);
                            graph.AddElement(edge);
                        }
                    }
                } 
            }
            return graph;
        }
        [Serializable]
        public class Node_data
        {
            [SerializeField] public node.Main_Info M_info;
            [SerializeField] public List<node.Info> message = new();
            public Node_data(node node)
            {
                M_info = node.M_info;
                M_info.position = node.GetPosition();
                
                message.Clear();

                for (int i = 0; i < node.outputContainer.childCount; i++)
                {
                    node.info.Add(new node.Info());
                    var info = new node.Info(node.info[i]);
                    Port output = (Port)node.outputContainer[i];
                    if (output.connected)
                    {
                        var a = output.connections.ElementAt(0).input;
                        info.next_GUID = ((node)a.node).M_info.GUID;
                    }
                    message.Add(info);
                }
            }
            public node To_Node()
            {
                node node = new(M_info);
                node.SetPosition(M_info.position);
                node.title = M_info.Title;
                foreach (var a in message)
                {
                    node.info.Add(a);
                }
                node.AddChoicePort(message.Count() - 1);
                return node;
            }
        }
    }
}
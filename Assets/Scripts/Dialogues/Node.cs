using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Dialogue_Helper
{
    public class node : Node
    {
        public Main_Info M_info = new();
        public List<Info> info = new();
        public node(Type type)
        {
            M_info.type = type;
            M_info.GUID = Guid.NewGuid().ToString();
            M_info.Title = title = M_info.type.ToString();
            Changes();
        }
        public node(Main_Info info)
        {
            M_info = info;
            Changes();
        }
        public void Changes()
        {
            AddChoicePort();
            if (M_info.type > Type.Entry)
            {
                inputContainer.Add(GenerateNewPort(Direction.Input));
                if (M_info.type == Type.If)
                {
                    var button = new Button(() => AddChoicePort()) { text = "New option" };
                    titleContainer.Add(button);
                }
            }
            Refresh();
        }
        public void AddChoicePort(int i = 1)
        {
            for (int j = 0; j < i; j++)
                outputContainer.Add(GenerateNewPort(Direction.Output));
            Refresh();
        }
        public void Refresh()
        {
            RefreshExpandedState();
            RefreshPorts();
        }
        public Port GenerateNewPort(Direction dir, Port.Capacity cap = Port.Capacity.Multi)
        {
            return InstantiatePort(Orientation.Horizontal, dir, cap, typeof(Single));
        }
        #region SomeTypes
        public enum Type
        {
            Entry,
            Message,
            If
        }
        [Serializable]
        public struct Main_Info
        {
            public Type type;
            public string Title;
            public string GUID;
            public Rect position;
        }
        [Serializable]
        public struct Info
        {
            public string text;
            public Sprite BackgroundImage;
            public string next_GUID;
            public Character.Char_Info character;
            public Info(Info inf)
            {
                this = inf;
            }
        }
        #endregion
    }
}
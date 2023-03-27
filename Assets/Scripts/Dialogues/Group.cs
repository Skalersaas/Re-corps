using UnityEditor.Experimental.GraphView;
using System;
namespace Dialogue_Helper
{
    public class group : Group
    {
        public string GUID;
        public group()
        {
            title = "New group";
            GUID = Guid.NewGuid().ToString();
        }   
    }
}
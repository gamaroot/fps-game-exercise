using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace game
{
    [CustomEditor(typeof(PlayerController))]
    public class PlayerControllerEditor : Editor
    {
        private void OnSceneGUI()
        {
            var player = (PlayerController)target;

            using (new Handles.DrawingScope(Color.red))
            {
                Handles.DrawPolyLine(player.Waypoints);

                Handles.DrawSolidDisc(player.transform.position, player.transform.up, 5f);

                var labelStyle = new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 24
                };
                labelStyle.normal.textColor = Color.green;

                Handles.Label(player.transform.position, "  Player", labelStyle);
            }
        }
    }
}
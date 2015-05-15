// 
//     Copyright (C) 2015 CYBUTEK
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

namespace UltimateImageViewer
{
    using System;
    using UnityEngine;

    public class DialogWindow : WindowBehaviour
    {
        private const float WIDTH_MIN = 400.0f;
        public string Message = string.Empty;

        private Action onYesAction;

        public static void Create(string title, string message)
        {
            if (UltimateImageViewer.Instance == null)
            {
                return;
            }

            UltimateImageViewer.Instance.gameObject.AddComponent<DialogWindow>().Init(title, message, null);
        }

        public static void CreateYesNo(string title, string message, Action yes)
        {
            if (UltimateImageViewer.Instance == null)
            {
                return;
            }

            UltimateImageViewer.Instance.gameObject.AddComponent<DialogWindow>().Init(title, message, yes);
        }

        public void Init(string title, string message, Action yes)
        {
            Message = message;
            WindowTitle = title;
            onYesAction = yes;
        }

        protected override void OnWindow()
        {
            GUI.BringWindowToFront(GetInstanceID());
            GUILayout.Label(Message, new[] { GUILayout.MinWidth(WIDTH_MIN) });

            if (onYesAction == null)
            {
                if (GUILayout.Button("CLOSE"))
                {
                    Close();
                }
            }
            else
            {
                GUIHelper.Horizontal(() =>
                {
                    if (GUILayout.Button("YES"))
                    {
                        onYesAction.Invoke();
                        Close();
                    }
                    if (GUILayout.Button("NO"))
                    {
                        Close();
                    }
                });
            }
        }
    }
}
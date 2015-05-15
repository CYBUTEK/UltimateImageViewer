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
    using System.Collections;
    using UnityEngine;

    public abstract class WindowBehaviour : MonoBehaviour
    {
        public bool IsDraggable = true;
        public Rect WindowPosition;
        public string WindowTitle;

        public void CentreWindow()
        {
            StartCoroutine(CentreWindowCoroutine(this));
        }

        public void Close()
        {
            Destroy(this);
        }

        public void Hide()
        {
            enabled = false;
        }

        public void ResetWindow()
        {
            WindowPosition = new Rect(Screen.width, Screen.height, 0.0f, 0.0f);
            CentreWindow();
        }

        public void Show()
        {
            enabled = true;
        }

        public void Start()
        {
            ResetWindow();
        }

        protected virtual void OnDisable()
        {
            InputLocker.SetLockState(this, false);
        }

        protected virtual void OnGUI()
        {
            InputLocker.SetLockState(this, WindowPosition.Contains(Event.current.mousePosition));

            WindowPosition = GUILayout.Window(GetInstanceID(), WindowPosition, id =>
            {
                OnWindow();
                if (IsDraggable)
                {
                    GUI.DragWindow();
                }
            }, WindowTitle);

            WindowPosition = Utils.ClampRectToScreen(WindowPosition, 30.0f);
        }

        protected virtual void OnWindow() { }

        private static IEnumerator CentreWindowCoroutine(WindowBehaviour windowBehaviour)
        {
            yield return null;
            windowBehaviour.WindowPosition.center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }
    }
}
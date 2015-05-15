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
    using UnityEngine;

    public class SelectionWindow : WindowBehaviour
    {
        private const float WIDTH_CLR = 50.0f;
        private const float WIDTH_LOAD = 100.0f;
        private const float WIDTH_URL = 400.0f;

        private int bookmarkCount;
        private string url = string.Empty;

        protected override void OnGUI()
        {
            if (bookmarkCount > UltimateImageViewer.Bookmarks.Count)
            {
                WindowPosition.height = 0.0f;
            }
            bookmarkCount = UltimateImageViewer.Bookmarks.Count;

            base.OnGUI();
        }

        protected override void OnWindow()
        {
            GUIHelper.Horizontal(() =>
            {
                url = GUILayout.TextField(url, new[] { GUILayout.Width(WIDTH_URL) });
                if (GUILayout.Button("CLR", new[] { GUILayout.Width(WIDTH_CLR) }))
                {
                    url = string.Empty;
                }

                if (GUILayout.Button("LOAD", new[] { GUILayout.Width(WIDTH_LOAD) }) && string.IsNullOrEmpty(url) == false)
                {
                    ImageWindow.Create(url);
                }
            });

            for (int i = 0; i < UltimateImageViewer.Bookmarks.Count; ++i)
            {
                DrawBookmark(UltimateImageViewer.Bookmarks[i]);
            }

            if (GUILayout.Button("CLOSE"))
            {
                Hide();
            }
        }

        private static void DrawBookmark(ImageBookmark bookmark)
        {
            GUIHelper.Horizontal(() =>
            {
                GUILayout.Label(bookmark.Name, new[] { GUILayout.Width(WIDTH_URL) });
                if (GUILayout.Button("CLR", new[] { GUILayout.Width(WIDTH_CLR) }))
                {
                    UltimateImageViewer.RemoveBookmark(bookmark);
                }
                if (GUILayout.Button("LOAD", new[] { GUILayout.Width(WIDTH_LOAD) }))
                {
                    ImageWindow.Create(bookmark);
                }
            });
        }

        private void Awake()
        {
            WindowTitle = "Ultimate Image Viewer by CYBUTEK - " + AssemblyInfo.Version;
        }
    }
}
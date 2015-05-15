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

    public class ImageWindow : WindowBehaviour
    {
        private const float SCREEN_MAX_HEIGHT = 0.75f;
        private const float SCREEN_MAX_WIDTH = 0.75f;
        private const float WIDTH_BOOKMARK = 100.0f;
        private const float WIDTH_MIN_CLOSE = 100.0f;
        private const float WIDTH_NAME = 150.0f;
        private const float WIDTH_PROGRESS = 250.0f;

        private float downloadProgress;
        private bool hasDisplayedImage;
        private Texture2D image;
        private ImageBookmark imageBookmark;
        private bool isWindowResizing;

        public static void Create(string url)
        {
            if (UltimateImageViewer.Instance != null)
            {
                UltimateImageViewer.Instance.gameObject.AddComponent<ImageWindow>().Init(url);
            }
        }

        public static void Create(ImageBookmark bookmark)
        {
            if (UltimateImageViewer.Instance != null)
            {
                UltimateImageViewer.Instance.gameObject.AddComponent<ImageWindow>().Init(bookmark);
            }
        }

        public void Init(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Close();
                return;
            }

            imageBookmark = new ImageBookmark(url);
            WindowTitle = imageBookmark.Name;
            StartCoroutine(LoadImage());
        }

        public void Init(ImageBookmark bookmark)
        {
            if (bookmark == null)
            {
                Close();
                return;
            }

            imageBookmark = bookmark;
            WindowTitle = imageBookmark.Name;
            StartCoroutine(LoadImage());
        }

        protected override void OnGUI()
        {
            base.OnGUI();
            ResizeWindow();
        }

        protected override void OnWindow()
        {
            if (image == null)
            {
                GUILayout.Label("Loading image: " + (downloadProgress * 100.0f).ToString("0.00") + "%", new[] { GUILayout.Width(WIDTH_PROGRESS) });
                CloseButton();
            }
            else
            {
                if (hasDisplayedImage == false)
                {
                    hasDisplayedImage = true;
                    GUILayout.Box(image, GUIStyle.none, new[]
                    {
                        GUILayout.Width(Mathf.Clamp(image.width, 0.0f, Screen.width * SCREEN_MAX_WIDTH)),
                        GUILayout.Height(Mathf.Clamp(image.height, 0.0f, Screen.height * SCREEN_MAX_HEIGHT))
                    });
                }
                else
                {
                    GUILayout.Box(image, GUIStyle.none, new[] { GUILayout.MinWidth(0.0f), GUILayout.MinHeight(0.0f), GUILayout.MaxWidth(WindowPosition.width), GUILayout.MaxHeight(WindowPosition.height) });
                }
                GUIHelper.Horizontal(() =>
                {
                    imageBookmark.Name = GUILayout.TextField(imageBookmark.Name, new[] { GUILayout.Width(WIDTH_NAME) });
                    if (GUILayout.Button("BOOKMARK", new[] { GUILayout.Width(WIDTH_BOOKMARK) }))
                    {
                        UltimateImageViewer.AddBookmark(imageBookmark);
                    }
                    CloseButton();
                });
            }
        }

        private void CloseButton()
        {
            if (GUILayout.Button("CLOSE", new[] { GUILayout.MinWidth(WIDTH_MIN_CLOSE) }))
            {
                Close();
            }
        }

        private IEnumerator LoadImage()
        {
            WWW www = new WWW(imageBookmark.Url);

            while (www.isDone == false)
            {
                downloadProgress = www.progress;
                yield return null;
            }

            if (string.IsNullOrEmpty(www.error))
            {
                image = www.texture;
                ResetWindow();
            }
            else
            {
                Close();
            }
        }

        private void ResizeWindow()
        {
            Vector2 hotSpot = new Vector2(20.0f, 20.0f);
            Rect stretchRect = new Rect(WindowPosition.max.x - (hotSpot.x * 0.5f), WindowPosition.max.y - (hotSpot.y * 0.5f), hotSpot.x, hotSpot.y);

            if (stretchRect.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0) && isWindowResizing == false)
            {
                isWindowResizing = true;
                IsDraggable = false;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isWindowResizing = false;
                IsDraggable = true;
            }

            if (isWindowResizing)
            {
                WindowPosition.max = Event.current.mousePosition;
            }
        }
    }
}
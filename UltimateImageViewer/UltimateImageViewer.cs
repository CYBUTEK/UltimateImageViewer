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
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using UnityEngine;

    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class UltimateImageViewer : MonoBehaviour
    {
        public static SelectionWindow SelectionWindow;

        private static List<ImageBookmark> bookmarks = new List<ImageBookmark>();
        private static UltimateImageViewer instance;

        public static List<ImageBookmark> Bookmarks
        {
            get
            {
                return bookmarks;
            }
        }

        private static bool IsToggleKeyComboDown
        {
            get
            {
                return (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.I);
            }
        }

        public static void CloseAllImageWindows()
        {
            if (instance == null)
            {
                return;
            }

            ImageWindow[] imageWindows = instance.GetComponents<ImageWindow>();
            int windowCount = imageWindows.Length;
            for (int i = 0; i < windowCount; ++i)
            {
                Destroy(imageWindows[i]);
            }
        }

        public static void CreateImageWindow(string url)
        {
            if (instance != null)
            {
                instance.gameObject.AddComponent<ImageWindow>().SetImage(url);
            }
        }

        public static void CreateImageWindow(ImageBookmark bookmark)
        {
            if (instance != null)
            {
                instance.gameObject.AddComponent<ImageWindow>().SetImage(bookmark);
            }
        }

        public static void HideSelectionWindow()
        {
            if (SelectionWindow != null)
            {
                SelectionWindow.enabled = false;
            }
        }

        public static void LoadBookmarks()
        {
            string filePath = Path.Combine(AssemblyInfo.Directory, "Bookmarks.xml");

            if (File.Exists(filePath) == false)
            {
                return;
            }

            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                if (stream.CanRead == false)
                {
                    return;
                }

                XmlSerializer xmlSerialiser = new XmlSerializer(typeof(List<ImageBookmark>));
                bookmarks = xmlSerialiser.Deserialize(stream) as List<ImageBookmark>;
                stream.Close();
            }
        }

        public static void SaveBookmarks()
        {
            using (Stream stream = File.Open(Path.Combine(AssemblyInfo.Directory, "Bookmarks.xml"), FileMode.OpenOrCreate))
            {
                if (stream.CanWrite == false)
                {
                    return;
                }

                XmlSerializer xmlSerialiser = new XmlSerializer(typeof(List<ImageBookmark>));
                xmlSerialiser.Serialize(stream, bookmarks);
                stream.Close();
            }
        }

        public static void ShowSelectionWindow()
        {
            if (SelectionWindow != null)
            {
                SelectionWindow.enabled = true;
            }
            else if (instance != null)
            {
                SelectionWindow = instance.gameObject.AddComponent<SelectionWindow>();
            }
        }

        public static void ToggleSelectionWindow()
        {
            if (SelectionWindow != null)
            {
                SelectionWindow.enabled = !SelectionWindow.enabled;
            }
            else if (instance != null)
            {
                SelectionWindow = instance.gameObject.AddComponent<SelectionWindow>();
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        private void OnDisable()
        {
            SaveBookmarks();
        }

        private void OnEnable()
        {
            LoadBookmarks();
        }

        private void Update()
        {
            if (IsToggleKeyComboDown == false)
            {
                return;
            }

            ToggleSelectionWindow();
        }
    }
}
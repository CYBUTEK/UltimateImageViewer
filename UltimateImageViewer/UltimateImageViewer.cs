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

        public static List<ImageBookmark> Bookmarks
        {
            get
            {
                return bookmarks;
            }
        }

        public static UltimateImageViewer Instance { get; private set; }

        private static bool IsToggleKeyComboDown
        {
            get
            {
                return (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.I);
            }
        }

        public static void AddBookmark(ImageBookmark bookmark)
        {
            if (bookmarks.Contains(bookmark))
            {
                DialogWindow.Create("Could not add bookmark", "Bookmark \"" + bookmark.Name + "\" already exists!");
                return;
            }

            bookmarks.Add(bookmark);
            SaveBookmarks();
        }

        public static void CloseAllImageWindows()
        {
            if (Instance == null)
            {
                return;
            }

            ImageWindow[] imageWindows = Instance.GetComponents<ImageWindow>();
            int windowCount = imageWindows.Length;
            for (int i = 0; i < windowCount; ++i)
            {
                Destroy(imageWindows[i]);
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

            using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    XmlSerializer xmlSerialiser = new XmlSerializer(typeof(List<ImageBookmark>));
                    bookmarks = xmlSerialiser.Deserialize(stream) as List<ImageBookmark>;
                }
                catch
                {
                    DialogWindow.CreateYesNo("Ultimate Image Viewer - Error", "Ultimage Image Viewer encountered an error when trying to load the bookmarks file. File may be corrupt!\n\nWould you like to delete the bookmarks file and start fresh?", () =>
                    {
                        File.Delete(filePath);
                        DialogWindow.Create("Ultimate Image Viewer", "The \"bookmarks.xml\" file has been deleted.");
                    });
                }
                stream.Close();
            }
        }

        public static void RemoveBookmark(ImageBookmark bookmark)
        {
            bookmarks.Remove(bookmark);
            SaveBookmarks();
        }

        public static void SaveBookmarks()
        {
            string filePath = Path.Combine(AssemblyInfo.Directory, "Bookmarks.xml");

            File.Delete(filePath);

            using (Stream stream = File.Open(Path.Combine(AssemblyInfo.Directory, "Bookmarks.xml"), FileMode.OpenOrCreate, FileAccess.Write))
            {
                XmlSerializer xmlSerialiser = new XmlSerializer(typeof(List<ImageBookmark>));
                xmlSerialiser.Serialize(stream, bookmarks);
                stream.Close();
            }

            LoadBookmarks();
        }

        public static void ShowSelectionWindow()
        {
            if (SelectionWindow != null)
            {
                SelectionWindow.enabled = true;
            }
            else if (Instance != null)
            {
                SelectionWindow = Instance.gameObject.AddComponent<SelectionWindow>();
            }
        }

        public static void ToggleSelectionWindow()
        {
            if (SelectionWindow != null)
            {
                SelectionWindow.enabled = !SelectionWindow.enabled;
            }
            else if (Instance != null)
            {
                SelectionWindow = Instance.gameObject.AddComponent<SelectionWindow>();
            }
        }

        private static void Hide()
        {
            if (Instance != null)
            {
                Instance.gameObject.SetActive(false);
            }
        }

        private static void Show()
        {
            if (Instance != null)
            {
                Instance.gameObject.SetActive(true);
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
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
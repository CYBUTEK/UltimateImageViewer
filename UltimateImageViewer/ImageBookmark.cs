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
    using System.IO;

    public class ImageBookmark
    {
        public readonly string Url;
        public string Name;

        public ImageBookmark(string url)
        {
            url = url.Trim();

            if (File.Exists(url))
            {
                url = "file://" + url;
            }

            Url = Utils.AlignSlashes(url);
            Name = Utils.GetFileName(Url);
        }

        public ImageBookmark() { }
    }
}
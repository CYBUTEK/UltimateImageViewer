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
    using System.Linq;
    using UnityEngine;

    public static class Utils
    {
        public static string AlignSlashes(string url)
        {
            return url.Replace('\\', '/');
        }

        public static string GetFileName(string url)
        {
            return url.Split('/').Last();
        }

        public static Rect ClampRectToScreen(Rect rect, float border)
        {
            rect.x = Mathf.Clamp(rect.x, -rect.width + border, Screen.width - border);
            rect.y = Mathf.Clamp(rect.y, -rect.height + border, Screen.height - border);
            return rect;
        }
    }
}
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

    public static class InputLocker
    {
        private static List<object> inputLockingObjects = new List<object>();
        private static bool isLocking;

        public static void SetLockState(object inputLocker, bool locking)
        {
            if (locking)
            {
                AddLocker(inputLocker);
            }
            else
            {
                RemoveLocker(inputLocker);
            }

            if (inputLockingObjects.Count > 0 && isLocking == false)
            {
                isLocking = true;
                InputLockManager.SetControlLock(AssemblyInfo.FileInfo.Name);
            }
            else if (isLocking && inputLockingObjects.Count == 0)
            {
                isLocking = false;
                InputLockManager.RemoveControlLock(AssemblyInfo.FileInfo.Name);
            }
        }

        private static void AddLocker(object inputLocker)
        {
            if (inputLockingObjects.Contains(inputLocker))
            {
                return;
            }

            inputLockingObjects.Add(inputLocker);
        }

        private static void RemoveLocker(object inputLocker)
        {
            inputLockingObjects.Remove(inputLocker);
        }
    }
}
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

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Ultimate Image Viewer")]
[assembly: AssemblyCompany("CYBUTEK Solutions")]
[assembly: AssemblyProduct("Ultimate Image Viewer")]
[assembly: AssemblyCopyright("Copyright © CYBUTEK 2015")]
[assembly: ComVisible(false)]
[assembly: Guid("ba52aec7-dbdc-43e8-91c9-8a425496e341")]
[assembly: AssemblyVersion("0.1.0.0")]

public static class AssemblyInfo
{
    public static readonly string Directory;
    public static readonly FileInfo FileInfo;
    public static readonly string Location;
    public static readonly string Version;

    static AssemblyInfo()
    {
        Location = Assembly.GetExecutingAssembly().Location;
        FileInfo = new FileInfo(Location);
        Directory = FileInfo.DirectoryName;
        Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Collections.Specialized;

using GridUI.DataModel.Drawers;
// Colors
using SharpDX;

using PerfectFractalZoomer.Fractal.MandelbrotLib;

// The data model defined by this file serves as a representative example of a strongly-typed
// model that supports notification when members are added, removed, or modified.  The property
// names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs.

namespace GridUI.DataModel
{
    /// <summary>
    /// Creates a collection of groups and items with hard-coded content.
    /// 
    /// SampleDataSource initializes with placeholder data rather than live production
    /// data so that sample data is provided at both design-time and run-time.
    /// </summary>
    public sealed class DataSource
    {
        private static DataSource _dataSource = new DataSource();

        private ObservableCollection<DataGroup> _allGroups = new ObservableCollection<DataGroup>();
        public ObservableCollection<DataGroup> AllGroups
        {
            get { return this._allGroups; }
        }

        public static IEnumerable<DataGroup> GetGroups(string uniqueId)
        {
            if (!uniqueId.Equals("AllGroups")) throw new ArgumentException("Only 'AllGroups' is supported as a collection of groups");
            
            return _dataSource.AllGroups;
        }

        public static DataGroup GetGroup(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _dataSource.AllGroups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static DataItem GetItem(string uniqueId)
        {
            // Simple linear search is acceptable for small data sets
            var matches = _dataSource.AllGroups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public DataSource()
        {
            var group1 = new DataGroup("Group-1",
                    "Pixel art",
                    "Assets/DarkGray.png",
                    "Group Description: Pixel drawn figurines");
            group1.Items.Add(new PixelDrawer("Group-1-Item-1",
                    "White pixels",
                    "Assets/LightGray.png",
                    Color.White,
                    group1));
            group1.Items.Add(new PixelDrawer("Group-1-Item-2",
                    "Blue pixels",
                    "Assets/LightGray.png",
                    Color.Blue,
                    group1));
            this.AllGroups.Add(group1);

            var group2 = new DataGroup("Group-2",
                    "Line art",
                    "Assets/DarkGray.png",
                    "Group Description: Line drawn figurines");
            group2.Items.Add(new LineDrawer("Group-2-Item-1",
                    "White lines",
                    "Assets/LightGray.png",
                    Color.White,
                    group2));
            group2.Items.Add(new LineDrawer("Group-2-Item-2",
                    "Blue lines",
                    "Assets/LightGray.png",
                    Color.Blue,
                    group2));
            group2.Items.Add(new LineDrawer("Group-2-Item-3",
                    "Red lines",
                    "Assets/LightGray.png",
                    Color.Red,
                    group2));
            this.AllGroups.Add(group2);

            var fractals = new DataGroup("Fractals",
        "Fractals",
        "Assets/DarkGray.png",
        "Group Description: Fractals!");
            fractals.Items.Add(new StaticMandelbrotDrawer("Static Mandelbrot",
                    "Static Mandelbrot",
                    "Assets/LightGray.png",
                    Color.White,
                    -.875f,
                    0f,
                    3f,
                    16,
                    fractals));
            fractals.Items.Add(new StaticMandelbrotDrawer("Static Mandelbrot 2",
        "Static Mandelbrot",
        "Assets/LightGray.png",
        Color.White,
        -1.5f,
        0.2f,
        0.5f,
        128,
        fractals));
            fractals.Items.Add(new TrajectoryMandelbrotDrawer("Dynamic Mandelbrot",
        "Item Title: Static Mandelbrot",
        "Assets/LightGray.png",
        Color.White,
        new DynamicTrajectory(new StaticTrajectory(-1.3f, 0.1f, 0.5f), (Trajectory a) => new StaticTrajectory(a.getRCenter(), a.getICenter(), a.getWidth() *0.99f)),
        128,
        fractals));

            this.AllGroups.Add(fractals);
        }
    }
}

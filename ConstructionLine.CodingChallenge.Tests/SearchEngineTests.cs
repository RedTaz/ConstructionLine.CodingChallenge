﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        [Test]
        public void Test()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red},
                Sizes = new List<Size> {Size.Small}
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void SearchEngine_Search_Red_Small()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts.FirstOrDefault()?.Color == Color.Red);
            Assert.That(results.Shirts.FirstOrDefault()?.Size == Size.Small);

            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Small).Count == 1);
            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Medium).Count == 0);
            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Large).Count == 0);

            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Red).Count == 1);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Blue).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Yellow).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.White).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Black).Count == 0);
        }

        [Test]
        public void SearchEngine_Search_Red_No_Size()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts.FirstOrDefault()?.Color == Color.Red);
            Assert.That(results.Shirts.FirstOrDefault()?.Size == Size.Small);
            Assert.That(results.Shirts.Skip(1).FirstOrDefault()?.Color == Color.Red);
            Assert.That(results.Shirts.Skip(1).FirstOrDefault()?.Size == Size.Medium);

            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Small).Count == 1);
            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Medium).Count == 1);
            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Large).Count == 0);

            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Red).Count == 2);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Blue).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Yellow).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.White).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Black).Count == 0);
        }

        [Test]
        public void SearchEngine_Search_No_Color_Medium()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Medium }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts.FirstOrDefault()?.Color == Color.Blue);
            Assert.That(results.Shirts.FirstOrDefault()?.Size == Size.Medium);
            Assert.That(results.Shirts.Skip(1).FirstOrDefault()?.Color == Color.Red);
            Assert.That(results.Shirts.Skip(1).FirstOrDefault()?.Size == Size.Medium);

            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Small).Count == 0);
            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Medium).Count == 2);
            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Large).Count == 0);

            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Red).Count == 1);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Blue).Count == 1);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Yellow).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.White).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Black).Count == 0);
        }


        [Test]
        public void SearchEngine_Search_Red_Blue_Medium()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red, Color.Blue },
                Sizes = new List<Size> { Size.Medium }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts.FirstOrDefault()?.Color == Color.Blue);
            Assert.That(results.Shirts.FirstOrDefault()?.Size == Size.Medium);
            Assert.That(results.Shirts.Skip(1).FirstOrDefault()?.Color == Color.Red);
            Assert.That(results.Shirts.Skip(1).FirstOrDefault()?.Size == Size.Medium);

            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Small).Count == 0);
            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Medium).Count == 2);
            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Large).Count == 0);

            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Red).Count == 1);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Blue).Count == 1);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Yellow).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.White).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Black).Count == 0);
        }

        [Test]
        public void SearchEngine_Search_Red_Small_Medium()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Blue - Medium", Size.Medium, Color.Blue),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red)
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red },
                Sizes = new List<Size> { Size.Small, Size.Medium }
            };

            var results = searchEngine.Search(searchOptions);

            Assert.That(results.Shirts.FirstOrDefault()?.Color == Color.Red);
            Assert.That(results.Shirts.FirstOrDefault()?.Size == Size.Small);
            Assert.That(results.Shirts.Skip(1).FirstOrDefault()?.Color == Color.Red);
            Assert.That(results.Shirts.Skip(1).FirstOrDefault()?.Size == Size.Medium);

            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Small).Count == 1);
            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Medium).Count == 1);
            Assert.That(results.SizeCounts.Single(s => s.Size == Size.Large).Count == 0);

            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Red).Count == 2);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Blue).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Yellow).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.White).Count == 0);
            Assert.That(results.ColorCounts.Single(c => c.Color == Color.Black).Count == 0);
        }
    }
}

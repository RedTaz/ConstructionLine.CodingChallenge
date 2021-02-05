using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {
            // if no color options provided, assume all colours are to be included
            List<Guid> colorIds = options.Colors.Any() ? options.Colors.Select(color => color.Id).ToList() : Color.All.Select(color => color.Id).ToList();

            // if no size options provided, assume all sizes are to be included
            List<Guid> sizeIds = options.Sizes.Any() ? options.Sizes.Select(size => size.Id).ToList() : Size.All.Select(size => size.Id).ToList();

            List<Shirt> shirts = new List<Shirt>();

            var colorCounts = Color.All.ToDictionary(color => color, color => new ColorCount() { Color = color, Count = 0 });
            var sizeCounts = Size.All.ToDictionary(size => size, size => new SizeCount() { Size = size, Count = 0 });

            foreach (Shirt shirt in _shirts)
            {
                if (colorIds.Contains(shirt.Color.Id) && sizeIds.Contains(shirt.Size.Id))
                {
                    shirts.Add(shirt);

                    colorCounts[shirt.Color].Count++;
                    sizeCounts[shirt.Size].Count++;
                }
            }

            return new SearchResults
            {
                Shirts = shirts,
                ColorCounts = colorCounts.Values.ToList(),
                SizeCounts = sizeCounts.Values.ToList()
            };

        }

    }
}
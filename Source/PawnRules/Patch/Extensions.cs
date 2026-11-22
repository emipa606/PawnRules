using System.Collections;
using System.Collections.Generic;
using PawnRules.Data;
using UnityEngine;
using Verse;

namespace PawnRules.Patch;

internal static class Extensions
{
    extension(string self)
    {
        public string Italic()
        {
            return $"<i>{self}</i>";
        }

        public string Bold()
        {
            return $"<b>{self}</b>";
        }
    }

    private static int LastIndex(this IList self)
    {
        return self.Count - 1;
    }

    extension(string self)
    {
        public int ToInt(int defaultValue = 0)
        {
            return int.TryParse(self, out var result) ? result : defaultValue;
        }

        public float ToFloat(float defaultValue = 0f)
        {
            return float.TryParse(self, out var result) ? result : defaultValue;
        }
    }

    extension(Pawn self)
    {
        public bool CanHaveRules()
        {
            return self is { Dead: false } && self.GetTargetType() != null;
        }

        public PawnType GetTargetType()
        {
            if (self == null)
            {
                return null;
            }

            if (Mod.Instance.Settings.NoAnimals && self.RaceProps.Animal)
            {
                return null;
            }

            if (self.Faction is { IsPlayer: true })
            {
                if (self.IsSlaveOfColony)
                {
                    return PawnType.Slave;
                }

                if (self.IsColonist)
                {
                    return PawnType.Colonist;
                }

                if (self.RaceProps.Animal)
                {
                    return PawnType.Animal;
                }
            }

            if (self.HostFaction is not { IsPlayer: true })
            {
                return null;
            }

            if (self.IsPrisonerOfColony)
            {
                return PawnType.Prisoner;
            }

            return self.IsSlaveOfColony ? PawnType.Slave : PawnType.Guest;
        }

        public Rules GetRules()
        {
            return Registry.GetRules(self);
        }
    }

    public static string GetDisplayName(this Presetable self)
    {
        return self == null ? Lang.Get("Preset.None") : self.Name ?? Lang.Get("Preset.Personalized");
    }

    extension(Rect self)
    {
        public Rect AdjustedBy(float x, float y, float width, float height)
        {
            return new Rect(self.x + x, self.y + y, self.width + width, self.height + height);
        }

        private Rect Round()
        {
            return new Rect(Mathf.Round(self.x), Mathf.Round(self.y), Mathf.Round(self.width),
                Mathf.Round(self.height));
        }

        public Rect[] GetHGrid(float padding, params float[] widths)
        {
            var unfixedCount = 0;
            var currentX = self.x;
            var fixedWidths = 0f;

            var rects = new List<Rect> { self };

            for (var index = 0; index < widths.Length; index++)
            {
                var width = widths[index];
                if (width >= 0f)
                {
                    fixedWidths += width;
                }
                else
                {
                    unfixedCount++;
                }

                if (index != widths.LastIndex())
                {
                    fixedWidths += padding;
                }
            }

            var unfixedWidth = unfixedCount > 0 ? Mathf.Max(0f, (self.width - fixedWidths) / unfixedCount) : 0f;

            foreach (var width in widths)
            {
                var newWidth = width >= 0f ? width : unfixedWidth;

                rects.Add(new Rect(currentX, self.y, newWidth, self.height).Round());

                currentX = Mathf.Min(self.xMax, currentX + newWidth + (newWidth > 0f ? padding : 0f));
            }

            return rects.ToArray();
        }

        public Rect[] GetVGrid(float padding, params float[] heights)
        {
            var unfixedCount = 0;
            var currentY = self.y;
            var fixedHeights = 0f;

            var rects = new List<Rect> { self };

            for (var index = 0; index < heights.Length; index++)
            {
                var height = heights[index];
                if (height >= 0f)
                {
                    fixedHeights += height;
                }
                else
                {
                    unfixedCount++;
                }

                if (index != heights.LastIndex())
                {
                    fixedHeights += padding;
                }
            }

            var unfixedHeight = unfixedCount > 0 ? Mathf.Max(0f, (self.height - fixedHeights) / unfixedCount) : 0f;

            foreach (var height in heights)
            {
                var newHeight = height >= 0f ? height : unfixedHeight;

                rects.Add(new Rect(self.x, currentY, self.width, newHeight).Round());

                currentY = Mathf.Min(self.yMax, currentY + newHeight + (newHeight > 0f ? padding : 0f));
            }

            return rects.ToArray();
        }
    }
}
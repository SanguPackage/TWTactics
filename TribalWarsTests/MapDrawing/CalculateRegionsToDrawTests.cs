using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using TribalWars.Maps.Drawing.Helpers;
using Xunit;

namespace TribalWarsTests.MapDrawing
{
    /// <summary>
    /// 
    /// </summary>
    public class CalculateRegionsToDrawTests
    {
        //[Fact]
        //public void MovingDownCausesTopGap()
        //{
        //    var old = new Rectangle(0, 0, 10, 10);
        //    var newR = new Rectangle(5, 0, 10, 10);

        //    var calcer = new Calculator(old, newR);
        //    var toDraw = calcer.GetNonOverlappingRectangles();

        //    var rectangleLeft = toDraw.First();

        //    Assert.Equal(0, rectangleLeft.Left);
        //    Assert.Equal(5, rectangleLeft.Right);

        //    Assert.Equal(0, rectangleLeft.Top);
        //    Assert.Equal(10, rectangleLeft.Bottom);
        //}

        [Fact]
        public void MovingRightCausesGapRight()
        {
            var old = new Rectangle(5, 5, 10, 10);
            var newR = new Rectangle(10, 5, 10, 10);

            var calcer = new RegionsToDrawCalculator(old, newR);
            Rectangle backgroundMove;
            var toDraw = calcer.GetNonOverlappingGameRectangles(out backgroundMove);

            var rectangleLeft = toDraw.First();

            Assert.Equal(old.Right, rectangleLeft.Left);
            Assert.Equal(20, rectangleLeft.Right);

            Assert.Equal(old.Top, rectangleLeft.Top);
            Assert.Equal(old.Bottom, rectangleLeft.Bottom);

            Assert.Equal(5, backgroundMove.X);
        }
    }
}

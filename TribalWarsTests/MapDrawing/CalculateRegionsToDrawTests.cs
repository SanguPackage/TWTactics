using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TribalWars.Maps.Drawing.Helpers;

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

        [Test]
        public void MovingRightCausesGapRight()
        {
            var old = new Rectangle(5, 5, 10, 10);
            var newR = new Rectangle(10, 5, 10, 10);

            var calcer = new RegionsToDrawCalculator(old, newR);
            Rectangle backgroundMove;
            var toDraw = calcer.GetNonOverlappingGameRectangles(out backgroundMove);

            var rectangleLeft = toDraw.First();

            Assert.AreEqual(old.Right, rectangleLeft.Left);
			Assert.AreEqual(20, rectangleLeft.Right);

			Assert.AreEqual(old.Top, rectangleLeft.Top);
			Assert.AreEqual(old.Bottom, rectangleLeft.Bottom);

			Assert.AreEqual(5, backgroundMove.X);
        }
    }
}

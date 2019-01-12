﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PracticingSMTP.Controllers
{
    public class GraphicsController : Controller
    { 
    [HttpGet]
        [ActionName("GetBase64Png")]
        public string GetBase64Png(string linesGraphicJSON,int width,int height)
        {
            return Draw2DLineGraphic(new JavaScriptSerializer().Deserialize<Signature>(linesGraphicJSON), width, height);
        }

        private string Draw2DLineGraphic(I2DLineGraphic lineGraphic, int width, int height)
        {
            //The png's bytes 
            byte[] png = null;

            //Create the Bitmap set Width and height 
            using (Bitmap b = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    //Make sure the image is drawn Smoothly (this makes the pen lines look smoother) 
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    //Set the background to white 
                    g.Clear(Color.White);

                    //Create a pen to draw the signature with 
                    Pen pen = new Pen(Color.Black, 2);

                    //Smooth out the pen, making it rounded 
                    pen.DashCap = System.Drawing.Drawing2D.DashCap.Round;

                    //Last point a line finished at 
                    Point LastPoint = new Point();
                    bool hasLastPoint = false;

                    //Draw the signature on the bitmap 
                    foreach (List<List<double>> line in lineGraphic.lines)
                    {
                        foreach (List<double> point in line)
                        {
                            var x = (int)Math.Round(point[0]);
                            var y = (int)Math.Round(point[1]);

                            if (hasLastPoint)
                            {
                                g.DrawLine(pen, LastPoint, new Point(x, y));
                            }

                            LastPoint.X = x;
                            LastPoint.Y = y;
                            hasLastPoint = true;
                        }
                        hasLastPoint = false;
                    }
                }

                //Convert the image to a png in memory 
                using (MemoryStream stream = new MemoryStream())
                {
                    b.Save(stream, ImageFormat.Png);
                    png = stream.ToArray();
                }
            }
            return Convert.ToBase64String(png);
        }

        public class Signature : I2DLineGraphic
        {
            public List<List<List<double>>> lines { get; set; }
        }

        interface I2DLineGraphic
        {
            List<List<List<double>>> lines { get; set; }
        }
    }
}
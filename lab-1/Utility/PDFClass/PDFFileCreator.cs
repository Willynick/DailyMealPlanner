using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using DailyMealPlanner.Business_Layer.UserData;
using DailyMealPlanner.Business_Layer;

namespace DailyMealPlanner.Utility.PDFClass
{
    public class PDFFileCreator
    {
        public static void GetPDFFile(User user, DailyRation dailyRation, Func<double> CalculateNumberOfCalories)
        {
            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font1 = new PdfStandardFont(PdfFontFamily.Helvetica, 40);
                PdfFont font2 = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
                //PdfFont font3 = new PdfStandardFont(PdfFontFamily.Helvetica, 15);

                PdfFont font3 = new PdfTrueTypeFont(new Font("Arial Unicode MS", 15), true);

                //Draw the text
                graphics.DrawString("Daily Food Ration", font1, PdfBrushes.DarkRed, new PointF(0, 0));

                PdfBitmap image = new PdfBitmap("../../Images/ImageForPDF.jpg");

                //Draw the image

                graphics.DrawImage(image, 250, 70);

                PdfPen pdfPen = new PdfPen(Color.DarkBlue, 2);

                graphics.DrawLine(pdfPen, 0, 180, 600, 180);

                graphics.DrawString("User Data", font2, PdfBrushes.DarkSlateBlue, new PointF(0, 195));

                graphics.DrawString("Weight: " + user.Weight, font3, PdfBrushes.Black, new PointF(0, 220));
                graphics.DrawString("Height: " + user.Height, font3, PdfBrushes.Black, new PointF(0, 240));
                graphics.DrawString("Age: " + user.Age, font3, PdfBrushes.Black, new PointF(0, 260));
                graphics.DrawString("Daily Activiry: " + user.dailyActivity, font3, PdfBrushes.Black, new PointF(0, 280));

                graphics.DrawString("ARM: " + user.userCalculations.ARM, font3, PdfBrushes.Black, new PointF(300, 220));
                graphics.DrawString("BMR: " + user.userCalculations.BMR, font3, PdfBrushes.Black, new PointF(300, 240));
                graphics.DrawString("Daily Calories Rate: " + user.userCalculations.DailyCaloriesRate, font3, PdfBrushes.Black, new PointF(300, 260));

                graphics.DrawLine(pdfPen, 0, 310, 600, 310);

                graphics.DrawString("MealTimes", font2, PdfBrushes.DarkSlateBlue, new PointF(0, 320));

                int height = 360;

                for (int i = 0; i < dailyRation.mealTimes.Count; i++)
                {
                    height = height + 10;

                    graphics.DrawString(dailyRation.mealTimes[i].name, font2, PdfBrushes.PaleVioletRed, new PointF(0, height));
                    for (int j = 0; j < dailyRation.mealTimes[i].products.Count; j++)
                    {
                        height = height + 20;

                        graphics.DrawString(dailyRation.mealTimes[i].products[j].Name + ": " + dailyRation.mealTimes[i].products[j].Gramms + " gramms", font3, PdfBrushes.Black, new PointF(0, height));
                        //graphics.DrawString(": " + dailyRation.mealTimes[i].products[j].Gramms + " gramms", font3, PdfBrushes.Black, new PointF(300, height));
                    }

                    height = height + 20;
                }

                graphics.DrawLine(pdfPen, 0, height + 20, 600, height + 20);

                graphics.DrawString("Total: " + Math.Round(CalculateNumberOfCalories(), 3) + " calories", font2, PdfBrushes.PaleVioletRed, new PointF(0, height + 40));

                //Save the document
                document.Save("DailyFoodRation.pdf");
                document.Close(true);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using easyhits4u.Domain;
using easyhits4u.Helper;
using System.IO;
using System.Net;

namespace easyhits4u.Code
{
    public class Helper
    {
        public static bool BitmapContains(Bitmap template, Bitmap bmp, out int pos)
        {
            pos = -1;
            AForge.Imaging.ExhaustiveTemplateMatching etm = new AForge.Imaging.ExhaustiveTemplateMatching(0.95f);

            AForge.Imaging.TemplateMatch[] tm = etm.ProcessImage(template, bmp);

            if (tm != null && tm.Length > 0)
            {
                float similarity = tm.Max(p => p.Similarity);
                AForge.Imaging.TemplateMatch result = tm.Where(p => p.Similarity == similarity).FirstOrDefault();
                if (result != null)
                {
                    if (result.Rectangle.X % bmp.Width == 0)
                        pos = result.Rectangle.X / bmp.Width;
                    else
                        pos = (result.Rectangle.X / bmp.Width) + 1;

                    return true;
                }
            }

            return false;
        }

        public static Bitmap[] GetBitmapData(string url)
        {
            WebClient web = new WebClient();

            Stream byteImg = web.OpenRead(url);
            Bitmap bmpImage = new Bitmap(byteImg);
            if (bmpImage != null && bmpImage.Width == 120 && bmpImage.Height == 44)
            {
                string tmp = string.Format("~/Data/Tmp/{0}.png", Guid.NewGuid().ToString());
                tmp = HttpContext.Current.Server.MapPath(tmp);
                bmpImage.Save(tmp);

                return new Bitmap[] { bmpImage };
            }

            Bitmap[] data = new Bitmap[5];

            for (int i = 0; i < 5; i++)
            {
                int index = i > 0 ? (i * 55 - 1) : 0;

                Rectangle cropArea = new Rectangle(index, 0, 55, 54);
                Bitmap bmpCrop = bmpImage.Clone(cropArea, PixelFormat.Format24bppRgb);
                data[i] = bmpCrop;
            }

            return data;
        }

        public static byte[] GetBytes(Bitmap bmp)
        {
            byte[] data;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                bmp.Save(stream, ImageFormat.Jpeg);
                stream.Position = 0;
                data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
                stream.Close();
            }

            return data;
        }

        public static void TrainData(string url)
        {
            Bitmap[] data = GetBitmapData(url);

            if (data != null)
            {
                foreach (Bitmap bm in data)
                {
                    Easyhits4u easy = new Easyhits4u();
                    easy.Name = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss ffff");
                    easy.Image = Helper.GetBytes(bm);
                    if (easy.Image != null)
                        easy.Length = easy.Image.Length;

                    Easyhits4u db = GetBitmapFromDB(bm, easy.Length);
                    if (db != null)
                    {
                        db.Count = db.Count + 1;
                        DomainManager.Update(db);
                    }
                    else
                        DomainManager.Insert(easy);
                }
            }
        }

        public static Easyhits4u GetBitmapFromDB(Bitmap bm, int length)
        {
            List<Easyhits4u> lst = HelperData.GeEasyhits4uByLength(length);
            if (lst != null)
            {
                foreach (Easyhits4u hit in lst)
                { 
                    Bitmap bmDB = GetBitmap(hit.Image);
                    int pos = -1;
                    if (bm != null)
                    {
                        if (BitmapContains(bmDB, bm, out pos))
                        {
                            return hit;
                        }
                    }
                        
                }
            }

            return null;
        }

        public static Bitmap GetBitmap(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            Image img = Image.FromStream(ms);
            Bitmap bm = new Bitmap(img);
            Rectangle rect = new Rectangle(0, 0, bm.Width, bm.Height);

            return bm.Clone(rect, PixelFormat.Format24bppRgb);
        }

        public static Easyhits4uType GetTheSameType(List<DataEntry> lst)
        {
            int count = lst.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    Easyhits4uType type1 = lst[i].Easyhits4u.Easyhits4uType;
                    Easyhits4uType type2 = lst[j].Easyhits4u.Easyhits4uType;
                    if (type1 != null && type2 != null && type1.Id == type2.Id)
                        return type2;
                }
            }

            return null;
        }
    }
}